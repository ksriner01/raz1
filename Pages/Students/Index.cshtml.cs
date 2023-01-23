//kriner-raz-3
//Replaced the original code within this file with the following code to add
//sorting capabilities to the Contoso University Web App.
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly SchoolContext _context;
        //kriner-raz-3
        //
        private readonly IConfiguration Configuration;

        public IndexModel(SchoolContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        //kriner-raz-3
        //The OnGetAsync method will receive a sortOrder parameter that can either
        //be Name or Date which is what the WebApp will sort the Students by in descending
        //order. Also added string searchString parameter to the OnGetAsync method which
        //would be the string that the user is searching for. Beginning of method then changed
        //to the following code below in order to accomodate the page function added to
        //the ContosoUniversity WebApp.
        public PaginatedList<Student> Students { get; set; }

        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            //kriner-raz-3
            //The search string is initialized and stored in the CurrentFilter property.
            CurrentFilter = searchString;

            //kriner-raz-3
            //Initializes an IQueryable<student> object which is modified in the switch statement
            //below. The switch statement accounts for different sorting methods such as by Name
            //in ascending and descending order or date in ascending or descending order.
            IQueryable<Student> studentsIQ = from s in _context.Students
                                             select s;

            //kriner-raz-3
            //An if statement that says if the searchString is not null or empty, in other words,
            //if someone has entered something in the search box, then provide the Students that
            //contain the string.
            if (!String.IsNullOrEmpty(searchString))
            {
                studentsIQ = studentsIQ.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstMidName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    studentsIQ = studentsIQ.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    studentsIQ = studentsIQ.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    studentsIQ = studentsIQ.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:
                    studentsIQ = studentsIQ.OrderBy(s => s.LastName);
                    break;
            }

            //kriner-raz-3
            //Sets the pageSize to 3 which is retrieved from the json configuration and 4
            //if configuration fails.
            var pageSize = Configuration.GetValue("PageSize", 4);
            Students = await PaginatedList<Student>.CreateAsync(
                studentsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}