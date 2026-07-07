using AutoMapper;
using Gym.BLL.ViewModels.SessionsViewModels;
using Gym.DAL.Entities;


namespace Gym.BLL.Utilites
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            SessionMapping();
        }

        private void SessionMapping()
        {

            CreateMap<Session, SessionViewModel>()
                .ForMember(m => m.CategoryName, opt => opt.MapFrom(s => s.Category.CategoryName))

                .ForMember(m => m.TrainerName, opt => opt.MapFrom(s => s.Trainer.Name))
                .ForMember(m => m.AvailableSlots, opt
                => opt.Ignore())
                .ReverseMap();
                

        }
    }
}