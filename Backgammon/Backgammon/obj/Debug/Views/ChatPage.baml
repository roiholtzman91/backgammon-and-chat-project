   M S B A M L   `   `   `  ����E  ABackgammon, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Backgammon.ViewsBackgammon.Views  5          Backgammon.Views.ChatPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:Backgammon.Views     5        N� Title$  
ChatWindow��5	       � DataContext 5
      �� *Chat+   Locator �� Source# [  .��5      � .+�5   
   �� $
��Left=�6[   $	��414��6v   $	��Top=�6�   $	��351��6�   $��0,36,0,0q�6�   ��6   �� *OnlineUsers+ �� SelectedItem 68   �� *
toClient+ �� View 5      �� 5      .P�5      �� 5      5      �� 5   
   $
��Left=�6   $��60��6F   $��351,320,0,0q�6R    �� TextWrapping$
 Wrap=�6g   $	��Top=�6{   $	��289��6�   ��6-   �� *	Message+6�   �� 5   
   $
��Send��6   $
��Left=�6@   $��640,320,0,0q�6[   $	��Top=�6p   $��90��6�   $��60��6�   ��6    �� *SendMassege+6�   �� 5   
   -   5         ��Name$ MessagesBlock��5      
�������$
��Left=�6]   $��
351,10,0,0q�6x   $
��Wrap=�6�   $	��Top=�6�   $	��310��6�   $	��449��6�   ��6>   �� *MessagesBlock+6�   �� 5   
   $��LogOff��6   $
��Left=�6=   $��351,380,0,0q�6X   $	��Top=�6m   $	��214��6�   $��70��6�   ��6"   �� *LogOff+6�   �� 5   
   $
��Play��6   $
��Left=�69   $��565,380,0,0q�6T   $	��Top=�6i   $	��235��6�   $��70��6�   ��6    �� *Play+6�   �� 5   
   $��	SendToAll��6   $
��Left=�6J   $��730,320,0,0q�6e   $	��Top=�6z   $��70��6�   $��60��6�   ��6%   �� *SendMassegeToAll+6�   �� 5   
   $��Online Users��6   $��20(�6(   
�������$
��Left=�6I   $��0,10,0,0q�6d   $
��Wrap=�6v   $	��Top=�6�   $��26��6�   $	��351��6�   5      5      