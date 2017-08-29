INCLUDE Irvine32.inc
ExitProcess proto,dwExitCode:dword

.data
input DWORD ?


RANGE_LOW	= 0;
RANGE_HIGH	= 100;
ARRAY_SIZE	= 10;

counter		BYTE  0
array		DWORD ARRAY_SIZE DUP(?)

header		BYTE "ASSIGNMENT #2",0Dh,0Ah,
				"------------------------------",0Dh,0Ah,
				"Member 1: John Lee",0Dh,0Ah,
				"Member 2: Sang Yong Jin",0Dh,0Ah,
				"Course Number: CPSC240",0Dh,0Ah,
				"This program generates 10 random numbers that users can search for",0Dh,0Ah,
				"------------------------------",0Dh,0Ah,0

tableHead	BYTE "Index     Data",0Dh,0Ah,0

spacer		BYTE  "         ",0

step1		BYTE "Step1: Populate the Array with random integers from 0 to 100",0Dh,0Ah,
				0Dh,0Ah,
				"Array of 10 numbers:",0Dh,0Ah,0
step2		BYTE "Step2: Sort the array",0Dh,0Ah,
					0Dh,0Ah,
					"Array of 10 numbers to be sorted:",0Dh,0Ah,0
step3		BYTE "Step3: Search the array",0Dh,0Ah,
					0Dh,0Ah,
					"Currently the array stores",0Dh,0Ah,0

msg1		BYTE "Found: ",0Ah,0Dh,0
msg2		BYTE "Not Found!",0Ah,0Dh,0
msg3		BYTE "End Of Program. ",0

prompt1		BYTE "Enter an integer to search: ",0 
prompt2		BYTE "Press any key to continue or 'Q' to Quit: ",0



.code
main PROC



call Clrscr
mov edx, OFFSET header
call WriteString
call crlf

mov edx, OFFSET step1
call WriteString
mov edx, OFFSET tableHead
call WriteString
call FillArray
call DisplayNumbers
call WaitMsg

call Clrscr

mov edx, OFFSET header
call WriteString
call crlf

mov edx, OFFSET step2
call WriteString
mov edx, OFFSET tableHead
call WriteString
call BubbleSort
call DisplayNumbers
call WaitMsg

Search:
call Clrscr

mov edx, OFFSET header
call WriteString
call crlf

mov edx, OFFSET step3
call WriteString
mov edx, OFFSET tableHead
call WriteString
call DisplayNumbers
mov edx, OFFSET prompt1
call WriteString
call ReadInt
mov input, eax
call BinarySearch

mov edx, OFFSET prompt2
call WriteString
call ReadChar

call CheckUpper

;call WaitMsg
call crlf	
cmp al,"Q"
jnz Search

call crlf
mov edx, OFFSET msg3
call WriteString
call WaitMsg

invoke ExitProcess,0
main ENDP

;--------------------------------------
CheckUpper PROC
; Generates: Adds 32 to convert the value to upper case.
; Recieves: eax = initial lowercase word
; Returns: eax = converted value
; Prerequisite: The value in eax must be lowercase

;-----------------------

cmp al,"q"
jnz SKIP
sub eax,32
SKIP:
ret

CheckUpper ENDP

;------------------------------------------------------------
FillArray PROC USES eax edi ecx edx
; Fills an array with a random sequence of 32-bit signed

; integers between LowerRange and (UpperRange - 1).
; Returns: nothing
;-----------------------------------------------------------
	mov eax,0
	mov	edi,OFFSET array	           ; EDI points to the array
	mov	ecx,ARRAY_SIZE	                ; loop counter
	mov	edx,RANGE_HIGH
	sub	edx,RANGE_LOW	           ; EDX = absolute range (0..n)
	cld                            ; clear direction flag

L1:	

	mov	eax,edx	                ; get absolute range
	call	RandomRange
	add	eax,RANGE_LOW	           ; bias the result
	stosd		                ; store EAX into [edi]
	loop	L1

	ret
FillArray ENDP

;----------------------------------------------------------
BubbleSort PROC USES eax ecx esi
; Sort an array of 32-bit signed integers in ascending order
; using the bubble sort algorithm.
; Receives: pointer to array, array size
; Returns: nothing
;-----------------------------------------------------------

	mov ecx,ARRAY_SIZE
	dec ecx			; decrement count by 1

L1:	push ecx			; save outer loop count
	mov	esi,OFFSET array	; point to first value

L2:	mov	eax,[esi]		; get array value
	cmp	[esi+4],eax	; compare a pair of values
	jge	L3			; if [esi] <= [edi], don't exch
	xchg eax,[esi+4]	; exchange the pair
	mov	[esi],eax

L3:	add	esi,4		; move both pointers forward
	loop	L2			; inner loop

	pop	ecx			; retrieve outer loop count
	loop L1			; else repeat outer loop

L4:	ret
BubbleSort ENDP

;-------------------------------------------------------------
BinarySearch PROC USES ebx edx esi edi
	
LOCAL first:DWORD,			; first position
	last:DWORD,				; last position
	mid:DWORD				; midpoint
;
; Search an array of signed integers for a single value.
; Receives: Pointer to array, array size, search value.
; Returns: If a match is found, EAX = the array position of the
; matching element; otherwise, EAX = -1.
;-------------------------------------------------------------
	mov	 first,0			; first = 0
	mov	 eax,ARRAY_SIZE		; last = (count - 1)
	dec	 eax
	mov	 last,eax
	mov	 edi,input			; EDI = searchVal
	mov	 ebx,OFFSET array			; EBX points to the array

L1: ; while first <= last
	mov	 eax,first
	cmp	 eax,last
	jg	 L5					; exit search

; mid = (last + first) / 2
	mov	 eax,last
	add	 eax,first
	shr	 eax,1
	mov	 mid,eax

; EDX = values[mid]
	mov	 esi,mid
	shl	 esi,2				; scale mid value by 4
	mov	 edx,[ebx+esi]		; EDX = values[mid]

; if ( EDX < input(EDI) )
;	first = mid + 1;
	cmp	 edx,edi
	jge	 L2
	mov	 eax,mid				; first = mid + 1
	inc	 eax
	mov	 first,eax
	jmp	 L4

; else if( EDX > searchVal(EDI) )
;	last = mid - 1;
L2:	cmp	 edx,edi
	jle	 L3
	mov	 eax,mid				; last = mid - 1
	dec	 eax
	mov	 last,eax
	jmp	 L4

; else return mid
L3:	
	push edx
	mov	 edx, OFFSET msg1
	call WriteString

	mov	 edx, OFFSET tableHead
	call WriteString

	mov eax,mid
	call WriteDec

	mov edx, OFFSET spacer
	call WriteString

	pop edx
	mov eax,edx
	call WriteDec
	call crlf

	mov	 eax,edi  				; value found
	call crlf
	jmp	 L9						; return (mid)

L4:	jmp	 L1						; continue the loop
	
L5:	
	mov	 eax,-1					; search failed
	mov	 edx, OFFSET msg2
	call WriteString
	call crlf
L9:	ret
BinarySearch ENDP

;------------------------------------------------------------
DisplayNumbers PROC USES eax edx ecx esi
; Displays the array of random numbers
; integers between LowerRange and (UpperRange - 1).
; Returns: nothing
;-----------------------------------------------------------
mov ecx, ARRAY_SIZE
mov esi, OFFSET array
mov counter,0
l1:
	mov eax,0

	mov al,counter
	call WriteDec

	inc counter

	mov edx, OFFSET spacer
	call WriteString

	mov eax,[esi]
	call WriteDec
	call crlf
	add esi,TYPE array
loop l1

ret
DisplayNumbers ENDP

end main