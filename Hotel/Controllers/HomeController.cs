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
        readonly IUserService userService;
        readonly IGuestService guestService;
        readonly ICategoryService categoryService;
        readonly ISettlementService settlementService;

        public HomeController(IRoomService roomService,ICategoryService categoryService, ISettlementService settlementService, IUserService userService, IGuestService guestService)
        {
            this.roomService = roomService;
            this.categoryService = categoryService;
            this.settlementService = settlementService;
            this.userService = userService;
            this.guestService = guestService;
        }

        public IActionResult Index()
        {
            RoomCategoryModel roomCategory = new RoomCategoryModel()
            {
                Rooms = roomService.GetRooms().Where(x => settlementService.GetSettlements().Any(y => y.RoomId != x.Id && y.StartDate < DateTime.Now)),
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

        [HttpPost]
        public IActionResult Booking(SettlementCreateModel settlementCreateModel)
        {
            var guestId = userService.FindUserByLogin(User.Identity.Name).GuestId;
            SettlementDTO settlementDTO = new SettlementDTO()
            {
                Id = Guid.NewGuid(),
                RoomId = settlementCreateModel.Room.Id,
                GuestId = guestId,
                StartDate = settlementCreateModel.Settlement.StartDate,
                EndDate = settlementCreateModel.Settlement.EndDate,
                CheckIn = true
            };

            var result = settlementService.AddSettlement(settlementDTO);
            if (result.Succedeed) 
            {
                return RedirectToAction("Index","Home");
            }

            ModelState.AddModelError("", result.Message);
            return View(settlementCreateModel.Room.Id);
        } 
    }
}
