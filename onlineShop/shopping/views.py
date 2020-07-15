from django.shortcuts import render, redirect, HttpResponse
from django.contrib.auth.forms import UserCreationForm
from shopping.forms import RegistrationForm, EditProfileForm
from django.contrib.auth.models import User
from django.contrib.auth.forms import UserChangeForm, PasswordChangeForm
from django.contrib.auth import update_session_auth_hash
from django.contrib.auth.decorators import login_required
from django.contrib.auth.views import LoginView, LogoutView, PasswordResetDoneView, PasswordResetView, PasswordResetConfirmView, PasswordResetDoneView, PasswordResetCompleteView
from shopping.models import *

# Create your views here.

def index(request):
    cateParents = Category.objects.filter(cate_parent_id=0)
    featureItems = Product.objects.filter(status=1)[:6]
    categoryTab = Product.objects.filter(status=True) [6:10]
    recommendedItems1 = Product.objects.filter(status=True) [10:13]
    recommendedItems2 = Product.objects.filter(status=True) [13:16]
    context = {
        'parents' : cateParents,
        'featureItems' : featureItems,
        'categoryTab' : categoryTab,
        'recommendedItems1' : recommendedItems1,
        'recommendedItems2' : recommendedItems2
    }
    return render(request, "shopping/index.html", context)
    # return render(request, 'shopping/index.html')

# def login(request):
# #     if request.method == 'POST':
# #         form = LoginView(request.POST)
# #         if form.is_valid():
# #             form.save()
# #             return redirect('/shopping/')
# #     else:
# #         form = LoginView()

# #         args = {'form':form}
# #         return render(request, 'shopping/login.html', args)
#     return render(request, 'shopping/login.html')
    

def Error(request):
    return render(request, 'shopping/404.html')

def blog(request):
    return render(request, 'shopping/blog.html')

def blog_single(request):
    return render(request, 'shopping/blog-single.html')

def cart(request):
    return render(request, 'shopping/cart.html')

def checkout(request):
    return render(request, 'shopping/checkout.html')

def contact_us(request):
    return render(request, 'shopping/contact-us.html')

def product_details(request):
    return render(request, 'shopping/product-details.html')

def shop(request):
    return render(request, 'shopping/shop.html')

def register(request):
    if request.method == 'POST':
        form = RegistrationForm(request.POST)
        if form.is_valid():
            form.save()
            return redirect("/register-done/") #thêm return ở đây thì k cần thêm href {% url %} cho action bên template (ngược lại - có url thì k cần dòng return)
            # response = HttpResponse('shopping/register-done')
            # return response
    else:
        form = RegistrationForm()

    return render(request, 'shopping/register.html', {'form': form})
    # return render(request, 'shopping/register.html')


def login_done(request):
    return render(request, 'shopping/login-done.html')

def register_done(request):
    return render(request, 'shopping/register-done.html')

def view_profile(request):
    args = {'user':request.user}
    return render(request, 'shopping/profile.html', args)

