   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  A =Doomie, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:DoomieDoomie    Doomie.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    4sys&clr-namespace:System;assembly=mscorlib    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     &localclr-namespace:Doomie     U QGongSolutions.WPF.DragDrop, Version=1.1.0.0, Culture=neutral, PublicKeyToken=nullddurn:gong-wpf-dragdrop  -     � Title$  Doomie��$	��380��$	��350��$	��500��$	��560�� � 	Resources    CheckBox_Load �� Orientation d� 	IsChecked  CheckBox_Merge �� 
TargetType �� Triggers P� Property �� Empty P� Value	 �� Property
 �� Value��%  &	        &	 Z     '�� �     �� .R��� $ 
Horizontal=�.+��� -   {��� *Wad_Enabled+ �� *
Wad_Load+�� .R��� $ 
Horizontal=�.+��� -   {��� *Wad_Enabled+ �� *	Wad_Merge+�� " �� P�  �@�ToolTip# Z . ��� 	 �@^�	IsEnabled
 .  .���  � RowDefinitions �� $
��Auto ��� .+�� $��0��.+�� -    �Name$ ProgressBar_Load�� � Minimum$ 0Q� � Maximum$	 100Q�$y�	Collapsed=��� -    ��Name$ TextBlock_Load��$��Center=�$��Center=�$y�	Collapsed=�� -    �Name$ 	Grid_Left��$��1��$��6 6 6 6q� � ColumnDefinitions �� $	��250 ��� $
��Auto ���  �� $��* �.+�� $��0�� �� $
��Auto ���  �� .+�� $��0��$��0�� ��  �� �� $
��Auto �.+��� $��0��$��0��$ Vertical=�.+��� -    ��Name$ Button_Playlist_New�� � ToolTip$" Creates a new empty Playlist��$��   New...   ��$��0 0 6 0q�$��Stretch=��� -   $ Button_Playlist_Open��$ Opens a Playlist��$��   Open...   ��$��0 6 6 0q�$��Stretch=��� -	   $ Button_Playlist_Import��$, &Imports all Playlists from a directory��$��   Import...   ��$��0 6 6 0q�$��Stretch=��� -
   $ Button_Playlist_Save��$$ Saves the selected Playlist(s)��$��
   Save   ��$��0 6 6 0q�$��Stretch=��� -   $ Button_Playlist_Save_As��$7 1Saves the selected Playlist(s) as new Playlist(s)��$��   Save As...   ��$��0 6 6 0q�$��Stretch=��� -   $ Button_Playlist_Clear��$* $Removes all listed Playlists entries��$��   Clear   ��$��0 6 6 0q�$��Stretch=��� $��0 6 6 0q�d� -    d�Name$ ToogleButton_Save_Session��$
 TrueY�$B <Persists list of loaded Playlists through different sessions��$��   Save Session   ��$��0 6 6 0q�$��Stretch=��� $��0��$��1��$ Vertical=�$��Bottom=�.+��� -   $ Button_Playlist_About��$��   About   ��$��0 0 6 0q�$��Stretch=��� -    ��Name$ ListView_Playlists�� J� 	AllowDrop . )  #GongSolutions.Wpf.DragDrop.DragDrop   IsDragSource .    IsDropTarget . $��0��$��2������ *
Playlist+   ItemContextMenu m�Name��%\   &	       �� .@�m� $&  ItemContextMenu_Playlists_Remove��-   $��Remove playlist(s)������ " ��.��� 	 �@�ContextMenu#
 [  �� View �� .P��� $�� �� �� CellTemplate �� .R��� $��16��$��16������ *Playlist_Icon+�� $��Playlist�� �� Width$
 Auto�� �� .R��� ���� *Playlist_Name+���� " �� ��  �� Binding �� *Playlist_Changed+ �� Value True.a��� 	 � ��$

 Bold"��� $��Files��$
 Auto�� �� DisplayMemberBinding �� *Playlist_Files+�� $��Location��$
 Auto�� �� *Playlist_Location+��   �� ResizeBehavior$  PreviousAndNext=�$��1��$��Right=�$��Stretch=�$��5��� $��2�� ��  �� $
��Auto ��� �� $
��Auto �.+�� $��0�� �� $
��Auto ���  �� �� �� .+��� $��Source Port:��$��0��$��0��$
��Bold"�$��Right=��� $��
Arguments:��$��0��$��1��$
��Bold"�$��Right=����� " ��.��� 	 � y�$
 	Collapsed=� ��  �� ! �� ElementName$! CheckBox_Parameters��" �� Path$" 	IsChecked� True.a��� 	 � y�$
 Visible=��� $��IWad:��$��0��$��2��$
��Bold"�$��Right=�� $��1��$��0�� �� �� $
��Auto ��� $
��Auto � �� .+��� -   # ��Name$# Label_SourcePort��$ ^� ShowOnDisabled$ .  $��0��$��0�� �� *" SelectedItem.Playlist_SourcePort+$! ListView_Playlists��% �� UpdateSourceTrigger$% PropertyChanged=�.���� -   $ TextBlock_SourcePort��$��CharacterEllipsis=����� *.,SelectedItem.Playlist_SourcePort_Description+$! ListView_Playlists��$% PropertyChanged=��� -   & ��Name$& CheckBox_Parameters��$��	Show Args��$��1��$��0��$��Center=�$��Center=�$��6 3 0 3q� �� *0.SelectedItem.Playlist_SourcePort_HasParameters+$! ListView_Playlists���� -   $ Button_SourcePort_Open��$ Selects the Source Port��$��	   ...   ��$��2��$��0��$��6 3 0 3q�� $��1��$��1�� ��  �� .+��� -   ' ��Name$' TextBox_Parameters��$��0��$��0��$��6 3 0 3q����� *-+SelectedItem.Playlist_SourcePort_Parameters+$! ListView_Playlists��$% PropertyChanged=�.������ " ��.��� 	 � y�$
 	Collapsed=� ��  �� $! CheckBox_Parameters��$" 	IsChecked� True.a��� 	 � y�$
 Visible=�.��� $��1��$��2�� �� �� $
��Auto � �� .+��� -   $# 
Label_IWad��$ .  $��0��$��0�� �� *SelectedItem.Playlist_IWad+$! ListView_Playlists��$% PropertyChanged=�.���� -   $ TextBlock_Iwad��$��CharacterEllipsis=����� *(&SelectedItem.Playlist_IWad_Description+$! ListView_Playlists��$% PropertyChanged=��� -   $ Button_Iwad_Open��$% Selects which IWad file to load��$��	   ...   ��$��1��$��0��$��6 3 0 6q��� -   $ ListView_Pwads�� .  .  . $��1��$��1q�$��0 0 0 0q����� *SelectedItem.Wadlist+$! ListView_Playlists�� ��%�   &	       �� .@�m� $' !ItemContextMenu_Pwads_Toogle_Load��-   $��Toogle load��{��� $" Wad_Enabled��� m� $" ItemContextMenu_Pwads_Remove��-   $��Remove Pwad(s)������ " ��.��� 	 �@�ContextMenu#
 [  �� .P��� $
��Load��# [  $
 Auto���� $
��PWad��$
 Auto�� �� *
Wad_File+�� $��Merge��# [ $
 Auto���� $��Status��$
 Auto�� �� *
Wad_Status+�� $��Location��$
 Auto�� �� *Wad_Location+� $��2�� �� �� $
��Auto � �� .+��� $��0��$��0��$ 
Horizontal=�.+��� -   $ Button_Pwad_Open��$&  Adds a PWad file to the Playlist��$��   Open...   ��$��0 6 6 0q��� -   $ Button_Pwad_Import��$E ?Imports all PWad files from a given directory into the Playlist��$��   Import...   ��$��0 6 6 0q��� -   $ Button_Pwad_Clear��$. (Removes all PWad files from the Playlist��$��   Clear   ��$��0 6 6 0q��� $��1��$��0��$ 
Horizontal=�$��Right=�.+��� -   $ Button_Play��$��
   Play   ��$��0 6 0 0q�