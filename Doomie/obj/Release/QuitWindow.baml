   M S B A M L   `   `   `  ���� 
  Doomie    Doomie.QuitWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       � Title$  Save changes��$	��320��$	��320��$	��100��$	��100�� � 
ResizeMode$ CanResizeWithGrip=� � ShowInTaskbar .   � WindowStartupLocation$ CenterOwner=� � 	Resources    Playlist_Save �� Orientation d� 	IsChecked��%K   &	        �� .R��� $ 
Horizontal=�.+���  �� *Playlist_Save+.��� $��6 6 6 6q� � RowDefinitions �� �� $
��Auto �.+��� -    ��Name$ ListView_Save��$��0��$��0������ *
Playlist+   ItemContextMenu	 m�Name��%U   &	       �� .@�m� $&	  ItemContextMenu_Selection_Toogle��-   $��Toogle save������ 
 �� 
TargetType"
 ��.���  �� Property �@�ContextMenu �� Value# [  �� View �� .P��� $
��Save�� �� CellTemplate# [   �� Width$
 Auto���� $��Playlist��$
 Auto�� �� .R��� ���� *Playlist_Name+�� $��Location��$
 Auto�� �� .R��� ���� *Playlist_Location+�� $ 
Horizontal=�$��Right=�$��0��$��1��.+��� -    ��Name$ Button_Continue�� �� 	IsDefault . $��   Continue   ��$��0 6 6 0q��� -   $ Button_Cancel�� �� IsCancel . $��   Cancel   ��$��0 6 0 0q�