.code
MyProc1 proc

    mov eax, ecx        ; Move first pixel to eax
    sub eax, edx        ; Subtract second pixel
    jns positive        ; If result is positive, skip negation
    neg eax             ; Convert to positive if negative
positive:
    
    ; Compare with threshold (e.g., 30)
    cmp eax, 30
    jl no_edge
    mov eax, 255        ; Edge detected - return white
    jmp done
no_edge:
    xor eax, eax        ; No edge - return black
done:
    ret
MyProc1 endp
end