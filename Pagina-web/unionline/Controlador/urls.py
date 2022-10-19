from django.urls import path
from django.contrib.auth import views as auth_views
from .views import inicio, login

urlpatterns = [
    #Links para redireccionar a otras paginas
    path('', inicio, name='inicio'),
    path('login', login, name='login'),
]
