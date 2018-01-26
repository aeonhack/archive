//------------------
//Creator: aeonhack
//Site: elitevs.net
//Created: 9/20/2012
//Changed: 9/20/2012
//Version: 1.0.0
//------------------
function rxor($data, $key) {
    $rawKey = array_merge(unpack('C*', $key));
    $rawData = array_merge(unpack('C*', $data));

    $n1 = 11;
    $n2 = 13;
    $ns = 257;

    $out = '';
    $keyLen = count($rawKey);

    for ($i = 0; $i < $keyLen; $i++) {
        $ns += $ns % ($rawKey[$i] + 1);
    }

    for ($i = 0; $i < count($rawData); $i++) {
        $ns = $rawKey[$i % $keyLen] + $ns;
        $n1 = ($ns + 5) * ($n1 & 255) + ($n1 >> 8);
        $n2 = ($ns + 7) * ($n2 & 255) + ($n2 >> 8);
        $ns = (($n1 << 8) + $n2) & 255;

        $out .= chr($rawData[$i] ^ $ns);
    }

    return $out;
}