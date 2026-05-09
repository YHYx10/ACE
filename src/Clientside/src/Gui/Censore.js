const words = ['ниг',
    'нег',
    'пиратка',
    'пидр',
    'пидор',
    'педик',
    'педрил',
    'гомик',
    'гей',
    'хохол',
    'хач',
    'талибан',
    'игил',
    'гидра',
    'даун',
    'аутист',
    'пизд',
    'конч',
    'инцел',
    'девствен',
    'симп',
    'nig',
    'neg',
    'piratka',
    'pidr',
    'pido',
    'pedik',
    'pedr',
    'gomik',
    'gey',
    'hohol',
    'hohl',
    'hach',
    'talib',
    'igil',
    'gidra',
    'daun',
    'autist',
    'pizda',
    'koncha',
    'incel',
    'devstven',
    'simp',
    'Пидар',
    'Пидарас',
    'Педарас',
    'пидараз',
    'pidaras',
    'pidaraz',
    'churka',
    'nacist',
    'nacik',
    'нацист',
    'hidjap',
    'холокост',
    'holocaust',
    'schutzstaffeln',
    'гитлер',
    'цыган',
    'cigan',
    'gipsy',
    'хидж',
    'хедж',
    'hidj',
    'hedj',
    'pussy'];

/**
 * @var {Array<{expression: RegExp, replacement: string}>}
 */
export const replacementMapping = [];

words.forEach(word => {
    const expression = new RegExp(word, 'gi');
    const replacement = '*'.repeat(word.length);

    replacementMapping.push({
        expression,
        replacement,
    });
});

export function censore(message) {
    replacementMapping.forEach(word => {
        message = message.replace(word.expression, word.replacement);
    });

    return message;
}
