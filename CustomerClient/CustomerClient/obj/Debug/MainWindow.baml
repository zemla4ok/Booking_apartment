   M S B A M L   `   `   `  ����I  ECustomerClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:CustomerClientCustomerClient  5   	       CustomerClient.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:CustomerClient     5   	   X TXceed.Wpf.Toolkit, Version=3.2.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	     � Title$  
MainWindow��5	   	   $	��500��6   $
��1000��6)    � 
ResizeMode$ NoResize=�66   .��5
      � .+�5   
   �� $
��Bold"�6   $��16(�6%   ���	#FFEB77FF$��
Apartments��6J   $
��Left=�6^   $	��449��6y   $	��Top=�6�   $	��822��6�   $��
160,10,0,0q�6�   .��5      � .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ 
Apartments��5      $��Right=�6/   $	��Top=�6K   $	��421��6c   $	��810��6p      AutoGenerateColumns .     
IsReadOnly . ��5      �� 5       �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6    .�5      -   5        5         Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$
 City��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 6;   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 6G   �� 
 �� Path$

 City�  5      $ Hotel��6-   $	 120 6<   	 6H   �� $
 Hotel�  5      $ Stars��6-   $ 50 6<   	 6G   �� $
 Stars�  5      $
 Cost��6-   $	 100 6;   	 6G   �� $

 Cost�  5      $ Places��6-   $	 100 6=   	 6I   �� $
 Places�  5      $ Free pl��6-   $	 100 6>   	 6J   �� $
 
FreePlaces�  5      $ 	Apart num��6-   $ 95 6@   	 6K   �� $
 ApartNumber�5      5      5      �� 5   
   $
��Bold"�6   $��16(�6%   ���	#FFEB77FF$��Filters��6J   $
��Left=�6\   $	��449��6w   $	��Top=�6�   $	��145��6�   $��	10,10,0,0q�6�   .��5       � .+�5!      �� .+�5"      �� $
��City��6     �� Padding$ 10,0,0,0q�6,   $��0,5,0,0q�6?   �� 5#      -   5        $
��City��5#      $��0,5,0,0q�6+   6<   �� 5%      $��Stars��6    $ 10,0,0,0q�6-   $��0,5,0,0q�6@   �� 5&      -   5        $��Stars��5&   1   {�.  $��0,5,0,0q�6>   6O   �� 5(      $��Places��6    $ 10,0,0,0q�6.   $��0,5,0,0q�6A   �� 5)      -   5        $��Places��5)   1   {�.  $��0,5,0,0q�6?   6P   �� 5+      $��Max cost��6    $ 10,0,0,0q�60   $��0,5,0,0q�6C   %  Xceed.Wpf.Toolkit.IntegerUpDown  5,      -   5        $
��Cost��5,   )   {�.  �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Minimum$ 0X�6G      Value$	 100X�6S   $��0,5,0,0q�6_   �� 5-      -   5        $��Filter��5-      {�.  $��35��6=   $��18(�6I   ���	#FF6B1D78���	#FFEB77FF$��0,10,0,0q�6�   $��Cost filter��6�   6�   5.      5/      50      51      52      