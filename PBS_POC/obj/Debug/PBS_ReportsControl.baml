   M S B A M L   `   `   `  ����  PBS_POC     PBS_POC.PBS_ReportsControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6dg-http://schemas.microsoft.com/wpf/2008/toolkit  5      .��5	      ]� 
��� � �.+�5
   
   �� $	��Top=�6   
��� � ���5      �� 5      
�������$��Report Duration��6+   .��5      �� .+�5      ��   �� Orientation$  
Horizontal=�6   $��5q�66   .+�5      �� $��From:��6   
�������$
��Left=�6?   $��12(�6Z   $
��Auto��6i   +  %Microsoft.Windows.Controls.DatePicker  5      -   5        $��dpFrom��5   $   $��12(�62   $
��Auto��6@   $	��Top=�6N   $
��Left=�5   0   $
��Auto��6K   �� 5      $	��To:��6   
�������$
��Left=�6=   $��12(�6X   $
��Auto��6f     5      -   5        $
��dpTo��5   $   $��12(�60   $
��Auto��6>   $	��Top=�6L   $
��Left=�6d   �� 5      -   5        $��btnGenerate��5      $��5,0,0,0q�61   $��Generate��6B   6U   �� 5      -   5        $��	btnExport��5   /   $��5,0,0,0q�6   $��Export��6@   $��Right=�6�   $	��Top=�5   )   $
��Auto��6A   $
��Auto��5   )   5      �� 5      $��2q�6   $  
Horizontal=�6(   $y�Hidden=�6A   .+�5      �� $��Data View :��6   �� 5      -   5        $��cmbViewTypeSelector��5      $
��Left=�6:    �� SelectedIndex$ 0��6U   .@�5      �� .��6'   Table6.   �� 5      .��6'   	Pie Chart62   �� 5       .��6'   	Bar Graph62   5!      5"      5#      5%      �� 5&   
   $
��Auto��6   $
��Auto��6#   $��Bottom=�60   ��5'      �� 5(      
�������$��Report��6+   .��5+      )  #Microsoft.Windows.Controls.DataGrid  -   5        $��
ReportGrid��5+         AutoGenerateColumns .    
IsReadOnly . $y�Visible=�6X      CanUserAddRows .     CanUserDeleteRows .  "   VerticalScrollBarVisibility$
 Auto=�5,   _   $   HorizontalScrollBarVisibility$
 Auto=�6�   $
��Left=�5.   I   ��. $
��Auto=�6�   $
��Auto=�5/   *   $
��Auto��6Z   $
��Auto��6h      RowHeaderWidth$ 0Q�50   "   ��5.   1   �� 51      54      56      57      