   M S B A M L   `   `   `  ����  cryptography    cryptography.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  
MainWindow��5   	   $	��301��6   $	��525��6)   #��� ��65    � 
ResizeMode$ CanMinimize=�6�   .��5      �  J� 	AllowDrop .  � ColumnDefinitions 5   
   �� 5      .+�5	   
   �� -   5         ��Name$ encryptedText��5	      $
��Left=�6)   $	��142��6D   $��	26,66,0,0q�6Q    �� TextWrapping$
 Wrap=�6d   $	��Top=�6x   $	��211��6�    . " �� VerticalScrollBarVisibility$
 Auto=�6�   �� 5
   
   -   5        $ decryptedText��5
      $
��Left=�6)   $	��142��6D   $��
262,66,0,0q�6Q   $
 Wrap=�6e   $	��Top=�6y   $	��221��6�   $
 Auto=�6�   �� 5   
   -   5         ��Name$ EncryptButton��5      $��Encrypt��6(   $
��Left=�6:   $��
26,226,0,0q�6U   $	��Top=�6i   $��75��6�    J� RenderTransformOrigin$ -0.387,-0.318&�6�   6�   �� 5   
   -   5        $ DecryptButton��5      $��Decrypt��6(   $
��Left=�6:   $��408,226,0,0q�6U   $	��Top=�6j   $��75��6�   6�   �� 5   
   -   5        $ keyField��5      $
��Left=�6$   $��23��6?   $��175,226,0,0q�6K   $
 Wrap=�6`   $	��Top=�6t   $	��202��6�   	 �� 	IsEnabled	 . $��1��6�   6�   �� 5   
   $��Default text:��6   $
��Left=�6(   $��	47,37,0,0q�6C   $	��Top=�6V   $	��147��6n   �� 5   
   $��Encoded text:��6   $
��Left=�6(   $��
308,38,0,0q�6C   $	��Top=�6W   $	��147��6o   �� 5   
   -   5        
 ��Name$
 keyLabel��5      $
��Key:��6"   $
��Left=�61   $��138,231,0,0q�6L   $	��Top=�6a   �� 5   
   -   5         ��Name$ 
methodList��5      $
��Left=�6'   $��
195,11,0,0q�6B   $	��Top=�6V   $	��182��6n   6z   �� 5   
   $��Encryption method:��6   $
��Left=�6-   $��	79,13,0,0q�6H   $	��Top=�6[   $ 0.536,0.438&�6s   $��21��6�   $	��116��6�   5      5      