﻿using System;
using System.Collections.Generic;
using System.Text;
using BookLibrary;

namespace BookTCPServer
{
    public class BookList
    {
        public static List<Book> bookList = new List<Book>()
        {
            new Book("Jitterbug Perfume","Tom Robbins",342,"9780553348989"),
            new Book("The Shining","Stephen King",450,"9780385121675"),
            new Book("The Haunting of Hill House","Shirley Jackson",288,"9780143122357")
        };
    }
}
