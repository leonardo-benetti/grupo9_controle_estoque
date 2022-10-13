using System;
using System.Collections.Generic;
using System.Linq;
using grupo9_controle_estoque.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace grupo9_controle_estoque.Controller;
public class NotificationController
{
    private readonly ApplicationDbContext context;
    private DbSet<Notification> Notification;
    public NotificationController(ApplicationDbContext context)
    {
        this.context = context;
        this.Notification = this.context.Notification;
    }
    public List<Notification> GetNotifications()
    {
        return this.Notification.ToList();
    }
    public void AddNotification(Notification notification)
    {
        this.Notification.Update(notification);
        this.context.SaveChanges();
    }
    public void DeleteNotification(Notification notification)
    {
        this.Notification.Remove(notification);
        this.context.SaveChanges();
    }
    public void EditNotification(Notification notification)
    {

    }
}
