using AutoMapper;
using Biker.Controllers.Resources;
using Biker.Models;
//using System.Collections.Generic;
using System.Linq;

namespace Biker.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // From Domain to API Resource
            CreateMap<Make, MakeResource>();
            CreateMap<Model, ModelResource>();
            CreateMap<Feature, FeatureResource>();
            CreateMap<Bike, BikeResource>()
             .ForMember(br => br.Contact, opt => opt.MapFrom(b => new ContactResource { Name = b.Contact.Name, Email = b.Contact.Email, Phone = b.Contact.Phone }))
             .ForMember(br => br.Features, opt => opt.MapFrom(b => b.Features.Select(bf => bf.FeatureId)));

            // From API Resource to Domain
            CreateMap<BikeResource, Bike>()
              .ForMember(b => b.Id, opt => opt.Ignore())
              .ForPath(b => b.Contact.Name, opt => opt.MapFrom(br => br.Contact.Name))
              .ForPath(b => b.Contact.Email, opt => opt.MapFrom(br => br.Contact.Email))
              .ForPath(b => b.Contact.Phone, opt => opt.MapFrom(br => br.Contact.Phone))
              .ForMember(b => b.Features, opt => opt.Ignore())
              .AfterMap((br, b) => {
                  // Remove unselected features
                  //var removedFeatures = new List<BikeFeature>();
                  //foreach (var f in b.Features)
                  //    if (!br.Features.Contains(f.FeatureId))
                  //        removedFeatures.Add(f);

                  //foreach (var f in removedFeatures)
                  //    b.Features.Remove(f);

                  //LINQ
                  var removedFeatures = b.Features.Where(f => !br.Features.Contains(f.FeatureId));
                  foreach (var f in removedFeatures)
                      b.Features.Remove(f);

                  // Add new features
                  //foreach (var id in br.Features)
                  //    if (!b.Features.Any(f => f.FeatureId == id))
                  //        b.Features.Add(new BikeFeature { FeatureId = id });

                  //LINQ
                  var addedFeatures = br.Features.Where(id => !b.Features.Any(f => f.FeatureId == id)).Select(id => new BikeFeature { FeatureId = id });
                  foreach (var f in addedFeatures)
                      b.Features.Add(f);
              });
        }
    }
}
