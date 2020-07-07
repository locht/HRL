# This is an auto-generated Django model module.
# You'll have to do the following manually to clean this up:
#   * Rearrange models' order
#   * Make sure each model has one field with primary_key=True
#   * Make sure each ForeignKey and OneToOneField has `on_delete` set to the desired behavior
#   * Remove `managed = False` lines if you wish to allow Django to create, modify, and delete the table
# Feel free to rename the models, but don't rename db_table values or field names.
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
    cate_parent_id = models.IntegerField(blank=True, null=True)
    name = models.TextField()
    description = models.TextField()
    status = models.BooleanField(blank=True, null=True)

    class Meta:
        managed = False
        db_table = 'Category'


class Order(models.Model):
    ship_name = models.TextField()
    ship_address = models.TextField()
    ship_phone = models.TextField()
    ordered_date = models.DateTimeField()
    total_amount = models.TextField()
    status = models.IntegerField()

    class Meta:
        managed = False
        db_table = 'Order'


class Orderdetail(models.Model):
    order_id = models.IntegerField()
    product_id = models.IntegerField()
    product_price = models.TextField()
    order_quantity = models.IntegerField()
    amount = models.TextField()

    class Meta:
        managed = False
        db_table = 'OrderDetail'


class Product(models.Model):
    cate_id = models.IntegerField()
    name = models.TextField()
    price = models.TextField()
    quantity = models.IntegerField()
    image = models.TextField()
    detail = models.TextField()
    status = models.BooleanField(blank=True, null=True)

    class Meta:
        managed = False
        db_table = 'Product'


class Promotion(models.Model):
    product_id = models.IntegerField()
    start_date = models.DateTimeField()
    end_date = models.DateTimeField()
    discount = models.TextField()

    class Meta:
        managed = False
        db_table = 'Promotion'


class AuthGroup(models.Model):
    name = models.CharField(unique=True, max_length=150)

    class Meta:
        managed = False
        db_table = 'auth_group'


class AuthGroupPermissions(models.Model):
    group = models.ForeignKey(AuthGroup, models.DO_NOTHING)
    permission = models.ForeignKey('AuthPermission', models.DO_NOTHING)

    class Meta:
        managed = False
        db_table = 'auth_group_permissions'
        unique_together = (('group', 'permission'),)


class AuthPermission(models.Model):
    content_type = models.ForeignKey('DjangoContentType', models.DO_NOTHING)
    codename = models.CharField(max_length=100)
    name = models.CharField(max_length=255)

    class Meta:
        managed = False
        db_table = 'auth_permission'
        unique_together = (('content_type', 'codename'),)


class AuthUser(models.Model):
    password = models.CharField(max_length=128)
    last_login = models.DateTimeField(blank=True, null=True)
    is_superuser = models.BooleanField()
    username = models.CharField(unique=True, max_length=150)
    first_name = models.CharField(max_length=30)
    email = models.CharField(max_length=254)
    is_staff = models.BooleanField()
    is_active = models.BooleanField()
    date_joined = models.DateTimeField()
    last_name = models.CharField(max_length=150)

    class Meta:
        managed = False
        db_table = 'auth_user'


class AuthUserGroups(models.Model):
    user = models.ForeignKey(AuthUser, models.DO_NOTHING)
    group = models.ForeignKey(AuthGroup, models.DO_NOTHING)

    class Meta:
        managed = False
        db_table = 'auth_user_groups'
        unique_together = (('user', 'group'),)


class AuthUserUserPermissions(models.Model):
    user = models.ForeignKey(AuthUser, models.DO_NOTHING)
    permission = models.ForeignKey(AuthPermission, models.DO_NOTHING)

    class Meta:
        managed = False
        db_table = 'auth_user_user_permissions'
        unique_together = (('user', 'permission'),)


class DjangoAdminLog(models.Model):
    action_time = models.DateTimeField()
    object_id = models.TextField(blank=True, null=True)
    object_repr = models.CharField(max_length=200)
    change_message = models.TextField()
    content_type = models.ForeignKey('DjangoContentType', models.DO_NOTHING, blank=True, null=True)
    user = models.ForeignKey(AuthUser, models.DO_NOTHING)
    action_flag = models.PositiveSmallIntegerField()

    class Meta:
        managed = False
        db_table = 'django_admin_log'


class DjangoContentType(models.Model):
    app_label = models.CharField(max_length=100)
    model = models.CharField(max_length=100)

    class Meta:
        managed = False
        db_table = 'django_content_type'
        unique_together = (('app_label', 'model'),)


class DjangoMigrations(models.Model):
    app = models.CharField(max_length=255)
    name = models.CharField(max_length=255)
    applied = models.DateTimeField()

    class Meta:
        managed = False
        db_table = 'django_migrations'


class DjangoSession(models.Model):
    session_key = models.CharField(primary_key=True, max_length=40)
    session_data = models.TextField()
    expire_date = models.DateTimeField()

    class Meta:
        managed = False
        db_table = 'django_session'


class ShoppingUserprofile(models.Model):
    description = models.CharField(max_length=100)
    user = models.OneToOneField(AuthUser, models.DO_NOTHING)
    city = models.CharField(max_length=100)
    phone = models.IntegerField()
    website = models.CharField(max_length=200)

    class Meta:
        managed = False
        db_table = 'shopping_userprofile'
