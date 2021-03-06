﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TogetherIsBetter.Model
{
    class Companies
    {

        private static TIB_Model db;

        public static void saveCompany(Company company)
        {
            try
            {
                using (db = new TIB_Model())
                {
                    if (company.Id != 0)
                    {
                        db.Company.Attach(company);
                        db.Entry(company).State = EntityState.Modified;                        
                    }
                    else
                    {
                        db.Company.Add(company);
                    }

                    db.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Company> getCompanies()
        {

            try
            {
                using (db = new TIB_Model())
                {
                    var query = from c in db.Company select c;

                    List<Company> companies = query.ToList();
                    return companies;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public static List<Reservation> getReservations(){

            try
            {
                using (db = new TIB_Model())
                {
                    var query = from r in db.Reservation
                                orderby r.StartDate
                                select r;

                    List<Reservation> reservations = query.ToList();
                    return reservations;
                }

            } catch(Exception ex){
                throw ex;
            }
        }


        public static void deleteCompany(Company company)
        {
            try
            {
                using (db = new TIB_Model())
                {
                    db.Company.Attach(company);
                    db.Company.Remove(company);
                    db.SaveChanges();

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
