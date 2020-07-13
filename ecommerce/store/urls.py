from django.conf.urls import url 

from . import views 

# app_name = 'store'
urlpatterns = [ 
    url(r'^$', views.store, name='store'),
    url(r'^cart/$', views.cart, name='cart'),
    url(r'^checkout/$', views.checkout, name='checkout'),
     
]