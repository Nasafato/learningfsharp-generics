let a = 123
box 1
box "abc"
let boxedString = box "abcdef"
(unbox<string> boxedString)
(unbox boxedString : string)



// euclid's formula for GCD
let rec gcd a b =
    if a = 0 then b
    elif a < b then gcd a (b - a)
    else gcd (a - b) b

gcd 10 20
gcd 100 144

let hcfGeneric (zero, sub, lessThan) =
    let rec hcf a b =
        if a = zero then b
        elif lessThan a b then hcf a (sub b a)
        else hcf (sub a b) b
    hcf

let hcfInt = hcfGeneric (0, (-), (<))
hcfInt 18 12

type Numeric<'T> =
    {Zero : 'T
     Subtract : ('T -> 'T -> 'T)
     LessThan : ('T -> 'T -> bool) }

let intOps = {Zero = 0; Subtract = (-); LessThan = (<)}
let bigintOps = {Zero = 0I; Subtract = (-); LessThan = (<)}
let int64Ops = {Zero = 0L; Subtract = (-); LessThan = (<)}
let hcfGeneric (ops : Numeric<'T>) =
    let rec hcf a b =
        if a = ops.Zero then b
        elif ops.LessThan a b then hcf a (ops.Subtract b a)
        else hcf (ops.Subtract a b) b
    hcf

let hcfInt = hcfGeneric intOps
let hcfBigInt = hcfGeneric bigintOps


type INumeric<'T> =
    abstract Zero: 'T
    abstract Subtract: 'T * 'T -> 'T
    abstract LessThan: 'T * 'T -> bool

let intOps =
    { new INumeric<int> with
        member ops.Zero = 0
        member ops.Subtract(x, y) = x - y
        member ops.LessThan(x, y) = x < y }



