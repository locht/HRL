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

class Category(models.Model):
    cate_parent_id = models.IntegerField(null=True)
    name = models.TextField()
    description = models.TextField()
    status = models.NullBooleanField()


    class Meta:
        db_table = 'Category'


class Order(models.Model):
    ship_name = models.TextField()
    ship_address = models.TextField()
    ship_phone = models.TextField()
    ordered_date = models.DateTimeField()
    total_amount = models.TextField()
    status = models.IntegerField()

    class Meta:
        db_table = 'Order'


class OrderDetail(models.Model):
    order_id = models.IntegerField()
    product_id = models.IntegerField()
    product_price = models.TextField()
    order_quantity = models.IntegerField()
    amount = models.TextField()
    class Meta:
        db_table = 'OrderDetail'


class Product(models.Model):
    cate_id = models.IntegerField()
    name = models.TextField()
    price = models.TextField()
    quantity = models.IntegerField()
    image = models.TextField()
    detail = models.TextField()
    status = models.NullBooleanField()
    class Meta:
        db_table = 'Product'



class ProductImage(models.Model):
    product_id = models.IntegerField()
    image_path = models.TextField()
    class Meta:
        db_table = 'ProductImage'



class Promotion(models.Model):
    product_id = models.IntegerField()
    start_date = models.DateTimeField()  # Field name made lowercase.
    end_date = models.DateTimeField()
    discount = models.TextField()

    class Meta:
        db_table = 'Promotion'
