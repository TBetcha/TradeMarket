using TradeMarket.IRepository;
using Dumpify;
using TradeMarket.Models.Dto;
using TradeMarket.Models;
using TradeMarket.Data;
using TradeMarket.Mappers;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using NodaTime;

namespace TradeMarket.Controllers{

    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger _logger;
        private readonly ApiResponse _response;
        private readonly IJwtUtils _jwtUtils;

        public ProductController(ILogger<ProductController> logger, ApplicationDbContext db, IJwtUtils jwtUtils)
        {
            _logger = logger;
            _response = new ApiResponse();
            _db = db;
            _jwtUtils = jwtUtils;
        }
}

