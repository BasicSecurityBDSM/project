   M S B A M L   `   `   `  ����E  AEncryption, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Encryption
Encryption  5   	       Encryption.Decrypteer   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:Encryption     5   	     � Title$  
Decrypteer��5   	   $��227.113��6   $��326.381��6-    � 
ResizeMode$ CanMinimize=�6=    � WindowStartupLocation$ CenterScreen=�6V    � WindowStyle$ 
ToolWindow=�6{   .��5	      � .+�5
   
   �� -   5         ��Name$ lblKey��5
      $��DesKey file��6    $
��Left=�66   $��	10,10,0,0q�6Q   $	��Top=�6d   �� 5   
   -   5        $ lblIV��5      $��
DesIV file��6   $
��Left=�64   $��	10,36,0,0q�6O   $	��Top=�6b   �� 5   
   -   5        $ lblHash��5      $��	Hash file��6!   $
��Left=�65   $��	10,62,0,0q�6P   $	��Top=�6c   �� 5   
   -   5        $ lblFile��5      $��Versleutelde file��6!   $
��Left=�6=   $��	10,88,0,0q�6X   $	��Top=�6k   �� 5   
   -   5         ��Name$ btnKey��5      $��	Browse...��6!   $
��Left=�65   $��
107,13,0,0q�6P   $	��Top=�6d   $��75��6|   6�   �� 5   
   -   5        $ btnIV��5      $��	Browse...��6    $
��Left=�64   $��
107,39,0,0q�6O   $	��Top=�6c   $��75��6{   6�   �� 5   
   -   5        $ btnHash��5      $��	Browse...��6"   $
��Left=�66   $��
107,65,0,0q�6Q   $	��Top=�6e   $��75��6}   6�   �� 5   
   -   5        $ btnFile��5      $��	Browse...��6"   $
��Left=�66   $��
107,91,0,0q�6Q   $	��Top=�6e   $��75��6}   6�   �� 5   
   -	   5        $ 	btnVorige��5      $��Vorige��6$   $��
10,163,0,0q�65    J� RenderTransformOrigin$ -2.249,-0.292&�6b   $��25��6�   $	��Top=�6�   $
��Left=�6�   $	��153��6�   �� 5   
   -
   5        $ 
btnDecrypt��5      $��
decrypteer��6%   $��
10,133,0,0q�6:   $ -2.249,-0.292&�6h   $��25��6�   $	��Top=�6�   $
��Left=�6�   $	��153��6�   5      5      