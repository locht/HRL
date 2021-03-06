from django.db import models
from django.contrib.auth.models import User
from django.db.models.signals import post_save

# Create your models here.
class UserProfile(models.Model):
    user = models.OneToOneField(User, on_delete=models.CASCADE)
    description = models.CharField(max_length=100, blank=True, default='')
    city = models.CharField(max_length=100, blank=True, default='')
    website = models.URLField(blank=True, default='')
    phone = models.IntegerField(default=0, blank=True)

def create_profile(sender,**kwargs ):
    if kwargs['created']:
        user_profile=UserProfile(user=kwargs['instance'])
        user_profile.save()

post_save.connect(create_profile, sender=User)