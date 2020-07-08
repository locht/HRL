from django import forms
from django.contrib.auth.models import User
from django.contrib.auth.forms import UserCreationForm, UserChangeForm
import re

# class RegistrationForm(UserCreationForm):
#     email = forms.EmailField(required=True)
#     username = forms.CharField(help_text=False)
#     class Meta:
#         model = User
#         fields = (
#             'username',
#             'email',
#             'password1',
#             'password2',
#         )
    
#     def save(self, commit=True):
#         user = super(RegistrationForm, self).save(commit=False)
#         user.email = self.cleaned_data['email']

#         if commit:
#             user.save()

#         return user

class EditProfileForm(UserChangeForm):
    # username = forms.CharField(help_text=False)

    class Meta:
        model = User
        fields = (
            'email',
            'password',
        )

# class RegistrationForm(forms.Form):
class RegistrationForm(UserCreationForm):
    username = forms.CharField(label='Username', max_length=30)
    email = forms.EmailField(label='Email')
    password1 = forms.CharField(label='Password', widget=forms.PasswordInput())
    password2 = forms.CharField(label='Password Confirm', widget=forms.PasswordInput())
    class Meta:
        model = User
        fields = (
            'username',
            'email',
            'password1',
            'password2',
        )

    def clean_password2(self):
        if 'password1' in self.cleaned_data:
            password1 = self.cleaned_data['password1']
            password2 = self.cleaned_data['password2']
            if password1 == password2 and password1:
                return password2
        raise forms.ValidationError("Invalid password")

    def clean_username(self):
        username = self.cleaned_data['username']
        if not re.search(r'^\w+$', username):
            raise forms.ValidationError("The account name has special characters")
        try:
            User.objects.get(username=username)
        except User.DoesNotExist:
            return username
        raise forms.ValidationError("Account already exists")

    def save(self, commit=True):
        print(self.cleaned_data)
        User.objects.create_user(
            username=self.cleaned_data['username'], 
            email=self.cleaned_data['email'], 
            password=self.cleaned_data['password1']
        )
