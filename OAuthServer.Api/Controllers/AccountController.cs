﻿using OAuthServer.Domain.Contracts;
using OAuthServer.Domain.Entities;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace OAuthServer.Api.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        IUserRepository _repository;

        public AccountController(IUserRepository repository)
        {
            _repository = repository;
        }

        // POST api/account/register
        [AllowAnonymous]
        [Route("register")]
        public Task<HttpResponseMessage> Register(User user)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                _repository.SaveOrUpdate(user);
                response = Request.CreateResponse(HttpStatusCode.Created, user);
            }
            catch (Exception)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, "Falha ao registrar o usuário");
                throw;
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }


        [HttpGet]
        //[Route("autenticado")]
        public IHttpActionResult Get()
        {
            var identity = (ClaimsIdentity)User.Identity;
            return Ok("Hello " + identity.Name);
        }


        protected override void Dispose(bool disposing)
        {
            _repository.Dispose();
        }
    }
}
