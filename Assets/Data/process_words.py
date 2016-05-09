fr = open('words.txt', 'r')
fw = open('words_processed.txt', 'w')
for line in fr:
	line = line.upper().strip()
	l = len(line)
	if line.isalpha() and l <= 6 and l >= 3:
		fw.write(line + '\n')
print('done.')
fr.close()
fw.close()
