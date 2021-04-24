using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Filps.Controllers
{
    public abstract class MediatorController : Controller
    {
        private IMediator _mediator;
        private IMapper _mapper;

        protected string Email => HttpContext.User.FindFirstValue(ClaimTypes.Email);
        protected ISession Session => HttpContext.Session;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetService<IMapper>();
    }
}