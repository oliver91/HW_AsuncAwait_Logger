using System.Web.Mvc;
using GreenHouse.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GreenHouse.Domain.Entities;

namespace GreenHouse.WebUI.Controllers
{

    public class HomeController : Controller
    {
        private IRoomRepository _repository;

        public HomeController(IRoomRepository productRepository)
        {
            _repository = productRepository;
        }

        public async Task<ActionResult> Index()
        {

            Logger.Instance.Write("Getting reservations from DB...");
            Reservation[,] resArr = new Reservation[15,8];
            IEnumerable<Reservation> res = null;
            await Task.Run(()=>
            {
               res = _repository.GetReservationsByDate(new DateTime(2015, 9, 8));
            });


            foreach (var item in res)
            {
                int roomNum = Int32.Parse(item.Room.Number);
                int timeRes = ((DateTime)item.EndTime).Hour;
                resArr[roomNum - 300, timeRes] = item;
            }
            return View(resArr);
        }
    }

}