from django.shortcuts import render

# Create your views here.

def home(request):
    numbers = [1,2,3,4,5]
    name = 'DevOps'

    args = {'myName': name ,'number': numbers}
    return render(request, 'account/login.html', args)
