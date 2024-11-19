.code
MyProc1 proc

; RCX = current pixel RGB values packed
    ; RDX = neighbor pixel RGB values packed
    
    ; Extract R, G, B components
    mov r8d, ecx        ; Current pixel
    mov r9d, edx        ; Neighbor pixel
    
    ; Calculate absolute differences for R
    mov eax, r8d
    and eax, 0FF0000h   ; Mask for R
    shr eax, 16
    mov r10d, r9d
    and r10d, 0FF0000h
    shr r10d, 16
    sub eax, r10d
    jns @f              ; Skip if positive
    neg eax
@@: mov r11d, eax       ; Store R difference
    
    ; Calculate absolute differences for G
    mov eax, r8d
    and eax, 0FF00h     ; Mask for G
    shr eax, 8
    mov r10d, r9d
    and r10d, 0FF00h
    shr r10d, 8
    sub eax, r10d
    jns @f
    neg eax
@@: add r11d, eax       ; Add G difference
    
    ; Calculate absolute differences for B
    mov eax, r8d
    and eax, 0FFh       ; Mask for B
    mov r10d, r9d
    and r10d, 0FFh
    sub eax, r10d
    jns @f
    neg eax
@@: add r11d, eax       ; Add B difference
    
    ; Compare with threshold and set edge value
    mov eax, r11d
    cmp eax, 50         ; Adjustable threshold
    jl no_edge
    mov eax, 255        ; Edge detected
    jmp done
no_edge:
    xor eax, eax        ; No edge
done:
    ret
MyProc1 endp
end