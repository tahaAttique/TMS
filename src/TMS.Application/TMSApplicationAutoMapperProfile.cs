using AutoMapper;
using TMS.Comments;
using TMS.TicketCategories;
using TMS.Tickets;

namespace TMS;

public class TMSApplicationAutoMapperProfile : Profile
{
    public TMSApplicationAutoMapperProfile()
    {
        CreateMap<TicketCategory, TicketCategoryDto>();
        CreateMap<Ticket, TicketDto>()
            .ForMember(x => x.TicketCategory, map => map.MapFrom(x => x.TicketCategory!.Name))
            .ForMember(x => x.TicketCategoryId, map => map.MapFrom(x => x.TicketCategory!.Id))
            .ForMember(x => x.CreatedDate, map => map .MapFrom(x => x.CreationTime))
            .ForMember(x => x.AssignedUserName, map => map.MapFrom(x => x.AssignedToUser!.UserName))
            .ForMember(x => x.SelfAssignedUserName, map => map.MapFrom(x => x.SelfAssignedUser!.UserName));
        CreateMap<KeyLookUp, KeyLookUpDto>();
        CreateMap<Comment, CommentDto>()
            .ForMember(x => x.UserName, map => map.MapFrom(x => x.User != null ? x.User.UserName : "Unknown"));
    }
}
