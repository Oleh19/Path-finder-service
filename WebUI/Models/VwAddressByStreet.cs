using ACP.BLL.Models;
using ACP.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class VwAddressByStreet
    {
        IGenericService<AddressDTO> context;
        int stritId;
        int currentPage;
        public VwAddressByStreet(IGenericService<AddressDTO> context, int stritId, int currentPage)
        {
            this.context = context;
            this.currentPage = currentPage;
            this.stritId = stritId;
            paging = new PagingInfo
            {
                ItemsPerPage = 10,
                CurrentPage = currentPage,
                TotalItems = context.GetAll().Where(c => c.StreetId == stritId).Count()
            };
            AddressDTOs = context.GetAll().ToList()
                .Where(c => c.StreetId == stritId)
                .OrderBy(c => c.StreetName)
                .Skip((currentPage - 1) * paging.ItemsPerPage)
                .Take(paging.ItemsPerPage)
                .Select(c => new AddressDTO
                {
                    AddressId = c.AddressId,
                    StreetId= c.StreetId,
                    StreetName= c.StreetName,
                    House= c.House,
                    SubdivisionName= c.SubdivisionName,
                    СountEntrance= c.СountEntrance,
                    СountFloor= c.СountFloor
                });
        }
        public PagingInfo paging { get; set; }
        public IEnumerable<AddressDTO> AddressDTOs
        {
            get;
            private set;
        }

    }
}