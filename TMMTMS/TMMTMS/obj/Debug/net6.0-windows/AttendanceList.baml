   M S B A M L   `   `   `  ����
  TMMTMS    TMMTMS.AttendanceList   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $	��720��5   	   $
��1080��6     � WindowStartupLocation$  CenterScreen=�6#    � WindowStyle$
 None=�6H    � AllowsTransparency$
 True��5   	   
������  � 	Resources 5	         	BoolToVis��%   &	        �� 5
   
   6'   5      .��5      �� -   5        ���#FFFFFF �� CornerRadius$ 15��5   "   ._�5   
   �  � ColumnDefinitions 5      �� 5      $	��200 �6#   �� 5      $��* �6#   .+�5      �  � RowDefinitions 5      �� 5      $��* �6$   �� 5      $��* �6$   .+�5      �� ���#E6E6E6$ 	10 10 0 0��6.   $��0,80,0,0q�6G   $��2��6Y   ._�5      ]� .+�5      �� $	��150��6!   $��80��6-    �� Padding$ 10q�69   $	��Top=�5      $��3,-65,-3,95q�62   ._�5       �� ��5!   "   �� 5"   &    �� ImageSource$h bD:\Stuff\Studium\HSMW\IF21wS1-B\4_c#\pruefungsbeleg\TMMTMS\TMMTMS\Images\TMM_Electric_Original.png��61   5$      5%      �� 5(      $��25,-65,25,95q�6$   $	��Top=�6:   $	��0.8��6O   ���#3333336\   �� 5+      $	��Top=�6%   .+�5,      �� -   5          
menuButton#��[ 5,   O   .��5-   "   �� 	 �� Orientation$	 
Horizontal=�6-   .+�5/   &   �� $
��Team��60   50   #   51      �� 52      #��[ 6%   .��53   "   �� $	 
Horizontal=�6-   .+�55   &   �� $��Anwesenheitsliste��60   56   #   57      �� 58      -   5        #��[ 58   I   .��59   "   �� $	 
Horizontal=�6-   .+�5;   &   �� $��	Protokoll��60   5<   #   5=      5>      5?      5@      5A      � 5D      $��1��6   $��20 70 20 20q�6#    5E      �� 5F      $
��auto �6$   �� 5G      $
��auto �6$   �� 5H      $
��auto �6$   �� 5I      $
��auto �6$   �� 5J      $
��auto �6$   �� 5K      $��* �6$   �� 5L      $
��auto �6$   �� 5M      $
��auto �6$   .+�5Q      � .+�5R      �� $��Anwesenheitsliste��6    $��32(�69   $��SemiBold"�6G   $
��Left=�5S      ���#12151867   5T      � 5V      $��1��6   .+�5W      &   System.Windows.Controls.DataGrid  -   5        $��datagrid_attendance��5W      
   AutoGenerateColumns$
 False��6:      Columns 5X      4  .System.Windows.Controls.DataGridTemplateColumn  5Y         CellTemplate 5Z   "   �� 5[   &   .R�5\   *   �� ��64   �� 5]   '   5_      5b      5c      �� 5e      $��75, 10q�6   $��6��6,   $	��0.8��69   ���#3333336F   �� 5g      $��7��6   $��8(�6)   ���#121518 �� TextAlignment$ Center=�6K   KITechnikum Mittweida Motorsport - University of Applied Sciences Mittweida�� 5h   `   1/Technikumplatz 17, 09648 Mittweida, Deutschland�� 5i   F   0.info@tm-motorsport.net - www.tm-motorsport.net5k      5m      5o      5p      5q      