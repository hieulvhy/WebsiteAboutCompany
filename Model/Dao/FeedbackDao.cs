using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class FeedbackDao
    {
        CompanyDbContext db = null;
        public FeedbackDao()
        {
            db = new CompanyDbContext();
        }
        public int Create(Feedback feedback)
        {
            feedback.CreateDate = DateTime.Now;
            feedback.ModifiedDate = DateTime.Now;
            db.Feedbacks.Add(feedback);
            db.SaveChanges();

            return feedback.ID;
        }
    }
}
