package main

import (
	"errors"
	"fmt"
	"log"
	"strconv"
	"strings"
)

func main() {
	fmt.Print("Enter value: ")
	amountFloat := inputValue()
	fmt.Println()

	amount := int(amountFloat * 100)

	coins := []int{5000, 2000, 1000, 500, 200, 100, 50, 20, 10, 5, 2}
	remainder, counts := valueToCoins(amount, coins)

	fmt.Println("Coins:")
	stringBuilder := strings.Builder{}
	for _, coin := range coins { // Iterating through `coins` instead of `counts` to keep original coins order
		if counts[coin] > 0 {
			stringBuilder.WriteString(fmt.Sprintf("%s: %d\n", coinToString(coin), counts[coin]))
		}
	}
	fmt.Println(stringBuilder.String())

	totalCoinValue := amount - remainder
	fmt.Printf("Total coin value: £%.2f\n", float64(totalCoinValue)/100)

	roundingErrorFloat := amountFloat - (float64(amount) / 100)
	remainderFloat := float64(remainder) / 100
	fmt.Printf("Remainder: £%f\n", roundingErrorFloat+remainderFloat)
}

func inputValue() (amountFloat float64) {
	_, err := fmt.Scanln(&amountFloat)
	if err != nil {
		if err.Error() == "strconv.ParseFloat: parsing \"\": invalid syntax" || err.Error() == "expected newline" {
			log.Fatal(errors.New("value must be a valid decimal value"))
		}
		if err.Error() == "unexpected newline" {
			log.Fatal(errors.New("value cannot be empty"))
		}
		log.Fatal(err)
	}
	return
}

// Assumes coins are sorted in descending order
func valueToCoins(amount int, coins []int) (remainder int, coinCounts map[int]int) {
	smallestCoin := coins[len(coins)-1]
	remainder = amount
	coinCounts = make(map[int]int, len(coins))
	for i := 0; i < len(coins) && remainder >= smallestCoin; i++ {
		coin := coins[i]
		coinQuantity := remainder / coin
		if coinQuantity > 0 {
			coinCounts[coin] = coinQuantity
		}
		remainder -= coinQuantity * coin
	}
	return
}

func coinToString(coin int) string {
	if coin >= 100 {
		return "£" + strconv.Itoa(coin/100)
	}

	return strconv.Itoa(coin) + "p"
}
