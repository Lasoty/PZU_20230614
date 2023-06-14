using AutoMapper;
using PzuCwiczenia.Infrastructure.ModelDto;
using PzuCwiczenia.WebApi.ViewModel;

namespace PzuCwiczenia.WebApi.Mappers;

public class BookViewModelProfile : Profile
{
    public BookViewModelProfile()
    {
        CreateMap<BookDto, BookViewModel>()
            //.ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
            //.ForMember(dst => dst.Title, opt => opt.Ignore())
            //.ForSourceMember(src => src.Author, opt => opt.DoNotValidate())
            //.ForMember(dst => dst.Borrower.Id, opt => 
            //{
            //    opt.Condition(src => src.Borrower != null);
            //    opt.MapFrom(src => src.Borrower.Id);
            //})
            .ReverseMap();

        CreateMap<CustomerDto, CustomerViewModel>()
            .ReverseMap();
    }
}
