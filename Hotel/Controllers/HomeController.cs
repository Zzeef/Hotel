using BLL.DTO;
using BLL.Interfaces;
using Hotel.WEB.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.WEB.Controllers
{
    public class HomeController : Controller
    {
        readonly IRoomService roomService;
        readonly ICategoryService categoryService;
        readonly ISettlementService settlementService;

        public HomeController(IRoomService roomService,ICategoryService categoryService, ISettlementService settlementService)
        {
            this.roomService = roomService;
            this.categoryService = categoryService;
            this.settlementService = settlementService;
        }

        public IActionResult Index()
        {
            RoomCategoryModel roomCategory = new RoomCategoryModel()
            {
                Rooms = roomService.GetRooms(),
                Categories = categoryService.GetCategories()
            };

            return View(roomCategory);
        }

        [HttpPost]
        public IActionResult Index(string name, int bed, decimal price, DateTime startDate, DateTime endDate)
        {
            var settlement = settlementService.GetSettlements();

            RoomCategoryModel result = new RoomCategoryModel()
            {
                Rooms = roomService.GetRooms(),
                Categories = categoryService.GetCategories()
            };

            if (name != null)
            {
                result.Rooms = result.Rooms.Where(x => result.Categories.Any(y => y.Id == x.CategoryId && name == y.Name));
            }
            if (bed != 0)
            {
                result.Rooms = result.Rooms.Where(x => result.Categories.Any(y => y.Id == x.CategoryId && y.Bed == bed));
            }
            if (price != 0)
            {
                result.Rooms = result.Rooms.Where(x => result.Categories.Any(y => y.Id == x.CategoryId && price == y.Price));
            }
            if (startDate >= DateTime.Now)
            {
                result.Rooms = result.Rooms.Where(x => !settlement.Any(y => y.RoomId == x.Id && startDate >= y.StartDate));
            }
            if (endDate >= DateTime.Now)
            {
                result.Rooms = result.Rooms.Where(x => !settlement.Any(y => y.RoomId == x.Id && endDate <= y.EndDate));
            }

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Booking(Guid id)
        {
            SettlementCreateModel result = new SettlementCreateModel();
            RoomDTO roomDto = await roomService.FindRoomByIdAsync(id);
            CategoryDTO categoryDto = await categoryService.FindCategoryByIdAsync(roomDto.CategoryId);
            result.Room = roomDto;
            result.Category = categoryDto;

            return View(result);
        }

        //[HttpPost]
        //public async Task<IActionResult> Booking(SettlementCreateModel settlementCreateModel)
        //{

        //}
    }
}
