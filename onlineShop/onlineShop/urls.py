"""onlineShop URL Configuration

The `urlpatterns` list routes URLs to views. For more information please see:
    https://docs.djangoproject.com/en/3.0/topics/http/urls/
Examples:
Function views
    1. Add an import:  from my_app import views
    2. Add a URL to urlpatterns:  path('', views.home, name='home')
Class-based views
    1. Add an import:  from other_app.views import Home
    2. Add a URL to urlpatterns:  path('', Home.as_view(), name='home')
Including another URLconf
    1. Import the include() function: from django.urls import include, path
    2. Add a URL to urlpatterns:  path('blog/', include('blog.urls'))
"""
# from django.contrib import admin
# from django.urls import path


# urlpatterns = [
#     path('admin/', admin.site.urls),
# ]

from django.conf.urls import include, url
from django.contrib import admin
from shopping import views
from django.contrib.auth.views import LoginView, LogoutView, PasswordResetDoneView, PasswordResetView, PasswordResetConfirmView, PasswordResetDoneView, PasswordResetCompleteView



urlpatterns = [
    url(r'^admin/', admin.site.urls),
    url(r'^shopping/', include('shopping.urls')),
    url(r'^$', views.index, name='index'),
    # url(r'^login/', views.login, name='login'),
    url(r'^checkout/', views.checkout, name='checkout'),
    url(r'^cart/', views.cart, name='cart'),
    url(r'^shop/', views.shop, name='shop'),
    url(r'^product-details/', views.product_details, name='product-details'),
    url(r'^blog/', views.blog, name='blog'),
    url(r'^blog-single/', views.blog_single, name='blog-single'),
    url(r'^Error/', views.Error, name='404'),
    url(r'^contact-us/', views.contact_us, name='contact-us'),
    url(r'^register/', views.register, name='register'),
    url(r'^login-done/', views.login_done, name='login-done'),
    url(r'^register-done/', views.register_done, name='register-done'),
    url(r'^login/$', LoginView.as_view(template_name='shopping/login.html'), name='login'),
    url(r'^profile/$', views.view_profile, name='profile'),

]