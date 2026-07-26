using System.Xml.Linq;
using SpecialRequest.Api.Models;

namespace SpecialRequest.Api.Repositories;

public class XmlSpecialRequestRepository : ISpecialRequestRepository
{
    private readonly string _filePath;

    public XmlSpecialRequestRepository()
    {
        _filePath = Path.Combine(AppContext.BaseDirectory, "Data", "SpecialRequests.xml");

        var directory = Path.GetDirectoryName(_filePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory!);
        }

        if (!File.Exists(_filePath))
        {
            var document = new XDocument(
                new XElement("SpecialRequests")
            );

            document.Save(_filePath);
        }
    }

    public IEnumerable<SpecialRequest> GetAll()
    {
        var document = XDocument.Load(_filePath);

        return document.Root!
            .Elements("SpecialRequest")
            .Select(x => new SpecialRequest
            {
                Id = (int)x.Element("Id")!,
                BookingId = (int)x.Element("BookingId")!,
                RequestType = (string?)x.Element("RequestType") ?? "",
                Description = (string?)x.Element("Description") ?? "",
                RequestDate = DateTime.Parse((string?)x.Element("RequestDate") ?? DateTime.Today.ToString("yyyy-MM-dd")),
                Status = (string?)x.Element("Status") ?? "Pending"
            })
            .ToList();
    }

    public SpecialRequest? GetById(int id)
    {
        return GetAll().FirstOrDefault(r => r.Id == id);
    }

    public SpecialRequest Add(SpecialRequest request)
    {
        var document = XDocument.Load(_filePath);

        int nextId = document.Root!
            .Elements("SpecialRequest")
            .Select(x => (int?)x.Element("Id") ?? 0)
            .DefaultIfEmpty(0)
            .Max() + 1;

        request.Id = nextId;

        document.Root!.Add(
            new XElement("SpecialRequest",
                new XElement("Id", request.Id),
                new XElement("BookingId", request.BookingId),
                new XElement("RequestType", request.RequestType),
                new XElement("Description", request.Description),
                new XElement("RequestDate", request.RequestDate.ToString("yyyy-MM-dd")),
                new XElement("Status", request.Status)
            )
        );

        document.Save(_filePath);

        return request;
    }

    public bool Update(SpecialRequest request)
    {
        var document = XDocument.Load(_filePath);

        var existing = document.Root!
            .Elements("SpecialRequest")
            .FirstOrDefault(x => (int)x.Element("Id")! == request.Id);

        if (existing == null)
            return false;

        existing.SetElementValue("BookingId", request.BookingId);
        existing.SetElementValue("RequestType", request.RequestType);
        existing.SetElementValue("Description", request.Description);
        existing.SetElementValue("RequestDate", request.RequestDate.ToString("yyyy-MM-dd"));
        existing.SetElementValue("Status", request.Status);

        document.Save(_filePath);

        return true;
    }

    public bool Delete(int id)
    {
        var document = XDocument.Load(_filePath);

        var existing = document.Root!
            .Elements("SpecialRequest")
            .FirstOrDefault(x => (int)x.Element("Id")! == id);

        if (existing == null)
            return false;

        existing.Remove();

        document.Save(_filePath);

        return true;
    }
}