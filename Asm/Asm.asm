.code
MyProc1 proc
    mov r8d, ecx        ; RCX = pierwszy piksel
    mov r9d, edx        ; RDX = drugi piksel
    
    ;Porownanie czesci R
    mov eax, r8d
    and eax, 0FF0000h   ; Maska dla R
    shr eax, 16
    mov r10d, r9d
    and r10d, 0FF0000h
    shr r10d, 16
    sub eax, r10d
    jns @f             
    neg eax
@@: mov r11d, eax      
    
    ;czesc G
    mov eax, r8d
    and eax, 0FF00h     ; Maska dla G
    shr eax, 8
    mov r10d, r9d
    and r10d, 0FF00h
    shr r10d, 8
    sub eax, r10d
    jns @f
    neg eax
@@: add r11d, eax       
    
    ;czesc B
    mov eax, r8d
    and eax, 0FFh       ;Maska dla B
    mov r10d, r9d
    and r10d, 0FFh
    sub eax, r10d
    jns @f
    neg eax
@@: add r11d, eax       
    
    ;ustawienie krawedzi
    mov eax, r11d
    cmp eax, 50         ;porownanie z progiem
    jl no_edge          ;Je?li mniejsze - brak kraw?dzi
    mov eax, 255        ;Je?li wi?ksze - wykryto kraw?d?
    jmp done
no_edge:
    xor eax, eax        
done:
    ret
MyProc1 endp
end