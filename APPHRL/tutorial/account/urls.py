from django.conf.urls import url
from . import views
from django.contrib.auth.views import LoginView, LogoutView, PasswordResetDoneView, PasswordResetView, PasswordResetConfirmView, PasswordResetDoneView, PasswordResetCompleteView

from django.urls import reverse_lazy

app_name = 'account'
urlpatterns = [
    url(r'^$', views.home),
    url(r'^login/$', LoginView.as_view(template_name='account/login.html'), name='login'),
    url(r'^logout/$', LogoutView.as_view(template_name='account/logout.html'), name='logout'),
    url(r'^register/$', views.register, name='register'),
    url(r'^profile/$', views.view_profile, name='profile'),
    url(r'^profile/edit/$', views.edit_profile, name='edit_profile'),
    url(r'^change-password/$', views.change_password, name='change_password'),

    url(r'^reset-password/$',PasswordResetView.as_view(
        template_name='account/reset_password.html',
        success_url=reverse_lazy('account:reset_password_done'),
        email_template_name='account/reset_password_email.html'
        ),name='reset_password'),

    url(r'^reset-password/confirm/(?P<uidb64>[0-9A-Za-z]+)-(?P<token>.+)/$',PasswordResetConfirmView.as_view(
        success_url=reverse_lazy('account:reset_password_complete'), #(?P<uidb64>[0-9A-Za-z]+)-(?P<token>.+)/
    ), name='reset_password_confirm'),
    url(r'^reset-password/complete/$',PasswordResetCompleteView.as_view(template_name='account/reset_password_complete.html'),name='reset_password_complete'),
    url(r'^reset-password/done/$',PasswordResetDoneView.as_view(template_name='account/reset_password_done.html'),name='reset_password_done'),
]