from django import forms
from django.contrib.auth.models import User
from django.contrib.auth.forms import UserCreationForm, UserChangeForm

class RegistrationForm(UserCreationForm):
    email = forms.EmailField(required=True)
    username = forms.CharField(help_text=False)
    class Meta:
        model = User
        fields = (
            'username',
            'email',
            'password1',
            'password2',
        )
    
    def save(self, commit=True):
        user = super(RegistrationForm, self).save(commit=False)
        user.email = self.cleaned_data['email']

        if commit:
            user.save()

        return user

class EditProfileForm(UserChangeForm):
    # username = forms.CharField(help_text=False)

    class Meta:
        model = User
        fields = (
            'email',
            'password',
        )
