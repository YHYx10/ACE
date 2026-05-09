export default[
    //info
    {        
        img: "info.svg",
        tab: "Info",
        items: [
            {
                img: "fm.svg",
                title: "information",
            },
        ]
    },
    //parents
    {
        img: "parents.svg",
        tab: "Parents",
        items: [
            {
                img: "father.svg",
                itemData:{
                    title:"father",
                    tag: "father",
                    folderm: "parents",
                    folderf: "parents",
                    items: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 42, 43, 44],
                    value: 0
                }
            },
            {
                img: "mother.svg",
                itemData:{
                    title:"mother",
                    tag: "mother",
                    folderm: "parents",
                    folderf: "parents",
                    items: [21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41,45],
                    value: 0
                }
            }
        ]
    },
    //dna
    {
        img: "dna.svg",
        tab: "Dna",
        items: [
            {
                img: "brow.svg",
                title: "brows",
                itemData:[                      
                    {
                        title:"Eyebrow height",
                        tag: "browHeight",                    
                        max: 1,
                        min: -1,
                        step: 0.1,
                        value: 0,
                    },
                    {
                        title:"The bulge of the eyebrows",
                        tag: "browWidth",                    
                        max: 1,
                        min: -1,
                        step: 0.1,
                        value: 0,  
                    }
                ]   
            },
            {
                img: "eye.svg",
                title: "eye",
                itemData:[                     
                   
                    {
                        title:"Eye cut ",
                        tag: "eyes",                    
                        max: 1,
                        min: -1,
                        step: 0.1,
                        value: 0, 
                    },
                    {
                        title:"Eye color ",
                        tag: "eyesColor",                    
                        max: 5,
                        min: 0,
                        step: 1,
                        value: 0,  
                    }
                ]   
            },
            {
                img: "nose.svg",
                title: "nose",
                itemData:[
                    {
                        title:"The width of the nose",
                        tag: "noseWidth",                    
                        max: 1,
                        min: -1,
                        step: 0.1,
                        value: 0,
                    },                
                    {
                        title:"The height of the nose",
                        tag: "noseHeight",                    
                        max: 1,
                        min: -1,
                        step: 0.1,
                        value: 0,
                    },
                    {
                        title:"Nose length ",
                        tag: "noseLength",                    
                        max: 1,
                        min: -1,
                        step: 0.1,
                        value: 0,  
                    },
                    {
                        title:"Gorbinka nose bridge ",
                        tag: "noseBridge",                    
                        max: 1,
                        min: -1,
                        step: 0.1,
                        value: 0,
                    },
                    {
                        title:"The height of the tip of the nose",
                        tag: "noseTip",                    
                        max: 1,
                        min: -1,
                        step: 0.1,
                        value: 0,  
                    },
                    {
                        title:"Treatment of the bridge of the nose",
                        tag: "noseBridgeShift",                    
                        max: 1,
                        min: -1,
                        step: 0.1,
                        value: 0, 
                    }
                ]   
            },
            {
                img: "cheek.svg",
                title: "cheek",
                itemData:[                      
                    {
                        title:"The height of the cheekbones",
                        tag: "cheekboneHeight",                    
                        max: 1,
                        min: -1,
                        step: 0.1,
                        value: 0,
                    },
                    {
                        title:"The width of the cheekbone",
                        tag: "cheekboneWidth",                    
                        max: 1,
                        min: -1,
                        step: 0.1,
                        value: 0,  
                    },
                    {
                        title:"Cheeks",
                        tag: "cheekWidth",                    
                        max: 1,
                        min: -1,
                        step: 0.1,
                        value: 0, 
                    }
                ]   
            },
            {
                img: "lips.svg",
                title: "lips",
                itemData:[                      
                    {
                        title:"Lip thickness",
                        tag: "lips",                    
                        max: 1,
                        min: -1,
                        step: 0.1,
                        value: 0,
                    }
                ]   
            },
            {
                img: "jaw.svg",
                title: "jaw",
                itemData:[                      
                    {
                        title:"Jaw width ",
                        tag: "jawWidth",                    
                        max: 1,
                        min: -1,
                        step: 0.1,
                        value: 0,
                    },
                    {
                        title:"The volume of the jaw",
                        tag: "jawHeight",                    
                        max: 1,
                        min: -1,
                        step: 0.1,
                        value: 0,
                    }
                ]   
            },
            {
                img: "chin.svg",
                title: "chin",
                itemData:[                      
                    {
                        title:"The height of the chin",
                        tag: "chinLength",                    
                        max: 1,
                        min: -1,
                        step: 0.1,
                        value: 0,
                    },
                    {
                        title:"Planting the chin",
                        tag: "chinPosition",                    
                        max: 1,
                        min: -1,
                        step: 0.1,
                        value: 0,  
                    },
                    {
                        title:"The width of the chin",
                        tag: "chinWidth",                    
                        max: 1,
                        min: -1,
                        step: 0.1,
                        value: 0, 
                    },
                    {
                        title:"The dimple of the chin",
                        tag: "chinShape",                    
                        max: 1,
                        min: -1,
                        step: 0.1,
                        value: 0,  
                    }
                ]   
            },
            {
                img: "neck.svg",
                title: "neck",
                itemData:[                      
                    {
                        title:"The thickness of the neck",
                        tag: "neckWidth",                    
                        max: 1,
                        min: 0,
                        step: 0.05,
                        value: 0,
                    }
                ]   
            }
        ]
    },    
    //hairs
    {
        img: "hairs.svg",
        tab: "Hairs",
        items: [
            {
                img: "hair.svg",
                itemData:{
                    title:"customiz:hair:1",
                    tag: "hair",
                    folderm: "mhairs",
                    folderf: "fhairs",
                    items: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14/*, 15, 16, 17, 18, 19, 20, 42, 43, 44*/],
                    value: 0,
                    color1: 0,
                    color2: 0,
                    secondColor: true,
                    randomFemale: true
                }
            },
            {
                img: "brow.svg",
                itemData:{
                    title:"customiz:hair:2",
                    tag: "eyebrows",
                    folderm: "brows",
                    folderf: "brows",
                    items: [0, 1, 2, 3, 4, 5, 6, 7, 8, /*9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 42, 43, 44*/],
                    value: 0,
                    color1: 0,
                    color2: 0,
                    secondColor: false,
                    randomFemale: true
                }
            },
            {
                img: "beard.svg",
                itemData:{
                    title:"customiz:hair:3",
                    tag: "beard",
                    folderm: "beards",
                    folderf: "beards",
                    items: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14/*, 15, 16, 17, 18, 19, 20, 42, 43, 44*/],
                    value: 0,
                    color1: 0,
                    color2: 0,
                    secondColor: true,
                    randomFemale: false
                }
            },
            {
                img: "neck.svg",
                itemData:{
                    title:"customiz:hair:4",
                    tag: "chest",
                    folderm: "chest",
                    folderf: "chest",
                    items: [-1, 0, 1, 2, 3, 4, 7, 8/*,, 16, 17, 18, 19, 20, 42, 43, 44*/],
                    value: -1,
                    color1: 0,
                    color2: 0,
                    secondColor: false,
                    randomFemale: false   
                }
            }
        ]
    },
    //makeup
    {
        img: "makeup.svg",
        tab: "Makeup",
        items: [
            {
                img: "makeup.svg",
                title: "makeup",
                itemData:{
                    title:"customiz:makeup:1",
                    tag: "makeup",                    
                    max: 15,
                    min: -1,
                    step: 1,
                    value: -1,
                    color1: 0,
                    color2: 0,
                    opacity: {
                        min: 0,
                        max: 1,
                        step: .05,
                        tag: "makeupOpacity",
                        value: 1
                    },
                    secondColor: true
                }   
            },
            {
                img: "blush.svg",
                title: "blush",
                itemData:{
                    title:"customiz:makeup:2",
                    tag: "blush",                    
                    max: 5,
                    min: -1,
                    step: 1,
                    value: -1,
                    color1: 0,
                    color2: 0,
                    opacity: {
                        min: 0,
                        max: 1,
                        step: .05,
                        tag: "blushOpacity",
                        value: 1
                    },
                    secondColor: false
                }   
            },
            {
                img: "lipstick.svg",
                title: "lipstick",
                itemData:{
                    title:"customiz:makeup:3",
                    tag: "lipstick",                    
                    max: 9,
                    min: -1,
                    step: 1,
                    value: -1,
                    color1: 0,
                    color2: 0,
                    opacity: {
                        min: 0,
                        max: 1,
                        step: .05,
                        tag: "lopstickOpacity",
                        value: 1
                    },
                    secondColor: false
                }   
            },
        ]
    },
    //complexion
    {
        img: "skin.svg",
        tab: "Skin",
        items: [
            {
                img: "jaw.svg",
                title: "skin",
                itemData:[                                       
                    {
                        title:"Redness of the skin",
                        tag: "complexion",                    
                        max: 11,
                        min: -1,
                        step: 1,
                        value: -1,
                    },
                    {
                        title:"Pigment spots",
                        tag: "sunDamage",                    
                        max: 10,
                        min: -1,
                        step: 1,
                        value: -1,
                    },
                    {
                        title:"Body skin disease ",
                        tag: "bodyBlemish",                    
                        max: 11,
                        min: -1,
                        step: 1,
                        value: -1,
                    },
                    {
                        title:"Facial skin disease",
                        tag: "blemish",                    
                        max: 23,
                        min: -1,
                        step: 1,
                        value: -1,
                    },
                    {
                        title:"Freckles and moles",
                        tag: "moles",                    
                        max: 17,
                        min: -1,
                        step: 1,
                        value: -1,
                    }  ,
                    {
                        title:"Skin aging",
                        tag: "ageing",                    
                        max: 14,
                        min: -1,
                        step: 1,
                        value: -1,
                    } 
                ]   
            }
        ]
    },
    //clothes
    {
        img: "clothes.svg",
        tab: "Clothes",
        items: [
            {
                img: "top.svg",
                itemData:{
                    title: "customiz:clth:1",
                    tag: "top",
                    folderm: "mtops",
                    folderf: "ftops",
                    items: [0,5,9,14],
                    value: 0,

                }
            },
            {
                img: "pants.svg",
                itemData:{
                    title: "customiz:clth:2",
                    tag: "pants",
                    folderm: "mpants",
                    folderf: "fpants",
                    items: [0,1,6,7],
                    value: 0,
                }
            },
            {
                img: "shoes.svg",
                itemData:{
                    title: "customiz:clth:3",
                    tag: "shoes",
                    folderm: "mshoes",
                    folderf: "fshoes",
                    items: [1,3,4,6],
                    value: 0,
                }
            }
        ]
    }   
]
