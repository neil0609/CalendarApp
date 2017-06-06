using CalendarApp.Domain;
using CalendarApp.Models.AddRequest;
using CalendarApp.Models.GetRequest;
using CalendarApp.Models.UpdateRequest;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CalendarApp.Services
{
    public class CalendarService : BaseService
    {
        // Inserts Item to the database
        public static int InsertCalendarItem(CalendarAddRequest model)
        {

            var id = 0;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.CalendarData_Insert"
                , inputParamMapper: delegate (SqlParameterCollection AddCalendarItem)
                {
                    AddCalendarItem.AddWithValue("@Title", model.Title);
                    AddCalendarItem.AddWithValue("@Location", model.Location);
                    AddCalendarItem.AddWithValue("@Description", model.Description);
                    AddCalendarItem.AddWithValue("@StartTime", model.StartTime);
                    AddCalendarItem.AddWithValue("@EndTime", model.EndTime);
                    AddCalendarItem.AddWithValue("@Date", model.EndTime);

                    SqlParameter p = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                    p.Direction = System.Data.ParameterDirection.Output;

                    AddCalendarItem.Add(p);

                }, returnParameters: delegate (SqlParameterCollection param)
                {
                    int.TryParse(param["@Id"].Value.ToString(), out id);
                });
            return id;
        }

        // Updates item in database by Id
        public static void UpdateCalendarItem(CalendarUpdateRequest model)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.CalendarData_Update"
                , inputParamMapper: delegate (SqlParameterCollection UpdateCalendarItem)
                {
                    UpdateCalendarItem.AddWithValue("@Id", model.Id);
                    UpdateCalendarItem.AddWithValue("@Title", model.Title);
                    UpdateCalendarItem.AddWithValue("@Location", model.Location);
                    UpdateCalendarItem.AddWithValue("@Description", model.Description);
                    UpdateCalendarItem.AddWithValue("@StartTime", model.StartTime);
                    UpdateCalendarItem.AddWithValue("@EndTime", model.EndTime);
                    UpdateCalendarItem.AddWithValue("@Date", model.Date);
                });
        }

        // Gets item from db by Id
        public static CalendarDomainItem GetCalendarItem(int id)
        {
            CalendarDomainItem item = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.CalendarData_SelectById"
                , inputParamMapper: delegate (SqlParameterCollection commentsCollection)
                {
                    commentsCollection.AddWithValue("@Id", id);
                }, map: delegate (IDataReader reader, short set)
                {
                    item = MapCalendar(reader);
                });
            return item;
        }

        // Gets List of Items in db
        public static List<CalendarDomainItem> GetCalendarItemList()
        {
            List<CalendarDomainItem> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.CalendarData_SelectAll",
                inputParamMapper: null, map: delegate (IDataReader reader, short set)
                {

                    CalendarDomainItem item = MapCalendar(reader);

                    if (list == null)
                    {
                        list = new List<CalendarDomainItem>();
                    }
                    list.Add(item);
                }
            );
            return list;
        }

        // Gets List of Items in db
        public static List<CalendarDomainItem> GetCalendarItemListByDate(DateTime Date)
        {
            List<CalendarDomainItem> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.CalendarData_SelectByDate",
                inputParamMapper: delegate (SqlParameterCollection commentsCollection)
                {
                    commentsCollection.AddWithValue("@Date", Date);
                }, map: delegate (IDataReader reader, short set)
                {
                    CalendarDomainItem item = MapCalendar(reader);

                    if (list == null)
                    {
                        list = new List<CalendarDomainItem>();
                    }
                    list.Add(item);
                }
            );
            return list;
        }

        // Deletes an Item in DB
        public static void DeleteCalendarItem(int id)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.CalendarData_DeleteById"
                , inputParamMapper: delegate (SqlParameterCollection commentsCollection)
                {
                    commentsCollection.AddWithValue("@Id", id);
                });
        }

        // Mapping the item. Using DRY methodology.
        public static CalendarDomainItem MapCalendar(IDataReader reader)
        {
            CalendarDomainItem item = new CalendarDomainItem();
            int startingindex = 0;

            item.Id = reader.GetInt32(startingindex++);
            item.Title = reader.GetString(startingindex++);
            item.Location = reader.GetString(startingindex++);
            item.Description = reader.GetString(startingindex++);
            item.StartTime = reader.GetDateTime(startingindex++);
            item.EndTime = reader.GetDateTime(startingindex++);
            item.Date = reader.GetDateTime(startingindex++);

            return item;
        }
    }
}