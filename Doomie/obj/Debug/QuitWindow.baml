   M S B A M L   `   `   `  ����
  Doomie    Doomie.QuitWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$  Save changes��5   	   $	��320��5   	   $	��320��5	   	   $	��100��5
   	   $	��100��5   	    � 
ResizeMode$ CanResizeWithGrip=�5   	    � ShowInTaskbar .   � WindowStartupLocation$ CenterOwner=�5   	    � 	Resources 5         Playlist_Save �� Orientation d� 	IsChecked��%�   &	        �� 5   
   .R�5      �� $ 
Horizontal=�6   .+�5      ��  5      �� *Playlist_Save+5      5      5      .��5      � $��6 6 6 6q�6    � RowDefinitions 5   
   �� 5      �� 5      $
��Auto �6   .+�5!   
   �� -   5         ��Name$ ListView_Save��5#      $��0��6$   $��0��64   ��5"      �� *
Playlist+ 5$        ItemContextMenu	 m�Name��%�   &	       �� 5%      .@�5'      m� $&	  ItemContextMenu_Selection_Toogle��5(      -   5        $��Toogle save��5)      5*      5,      5-      ��5/      �� 50      
 �� 
TargetType"
 ��6   .�51      ��  �� Property �@�ContextMenu �� Value# [ 64   52       �� View 55      �� 56      .P�57      �� $
��Save��58       �� CellTemplate# [  59       �� Width$
 Auto��6K   �� 5;      $��Playlist��5<      $
 Auto��6/    5=      �� 5>      .R�5?   "   �� ��5@   !   �� *Playlist_Name+5A   #   5B      5D      �� 5F      $��Location��5G      $
 Auto��6/    5H      �� 5I      .R�5J   "   �� ��5K   !   �� *Playlist_Location+5L   #   5M      5O      5Q      5S      �� 5U   
   $ 
Horizontal=�5V   	   $��Right=�6"   $��0��6>   $��1��6N   .+�5W      �� -   5         ��Name$ Button_Continue��5X       �� 	IsDefault . $��   Continue   ��5[      $��0 6 6 0q�6&   �� 5\      -   5        $ Button_Cancel��5]       �� IsCancel . $��   Cancel   ��5`      $��0 6 0 0q�6$   5a      5c      5e      