// JavaScript source code

function findSimilarWord(list, source) {
	var ranks = [];

	for (var item = 0; item < list.length; item++) {
		var rank = compareStringsProcess(source, list[item]);
		var dict = [rank, list[item]];
		ranks.push(dict);
	}

	var result = ranks.sort(function (keyA, keyB) {
		if (keyA[0] < keyB[0]) return 1; // descending
		if (keyA[0] > keyB[0]) return -1;
		return 0;
	});;

	return (result[0])[1] + ' => ' + ((result[0])[0] * 100.0).toFixed(2) + '%';
}

function compareStringsProcess(source, listitem) {

	var pairs1 = wordPairs(source.toUpperCase());
	var pairs2 = wordPairs(listitem.toUpperCase());

	var intersection = 0;
	var union = pairs1.length + pairs2.length;

	for (var i = 0; i < pairs1.length; i++) {
		var pair1 = pairs1[i];

		for (var j = 0; j < pairs2.length; j++) {
			var pair2 = pairs2[j];

			if (pair1 === pair2) {
				intersection++;
				break;
			}
		}
	}
	return (2.0 * intersection) / union;
}

function wordPairs(str) {

	var allPairs = [];
	var words = str.split(" ");

	for (var w = 0; w < words.length; w++) {
		// Find the pairs of characters
		var pairsInWord = letterPairs(words[w]);

		for (var p = 0; p < pairsInWord.length; p++) {
			allPairs.push(pairsInWord[p]);
		}
	}

	return allPairs;
}

function letterPairs(str) {

	var numPairs = str.length - 1;
	var pairs = new Array(numPairs);

	for (var i = 0; i < numPairs; i++) {
		pairs[i] = str.substring(i, i + 2);
	}

	return pairs;
}
