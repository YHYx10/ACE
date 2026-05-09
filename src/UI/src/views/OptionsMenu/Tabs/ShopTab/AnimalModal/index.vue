<template>
  <div class="modal">
      <MainAnimalCard class="" @close="$emit('close')"/>
    <div class="centre-animal-part">
    </div>
    <div class="list">
      <div class="canvas">
        <div class="item" v-for="item in animalsList" :key="item.id">
          <div class="wrap" >
            <AnimalCard :animalCard="item" class="animal-card-modal" @animalModalClick="animalModalClick"/>
            <AnimalDescription :animalsDescription="item"/>
          </div>
        </div>
      </div>
      
    </div>
    
    <!-- <div class="animals-right-cards">
      <div class="animal-right-card" v-for="item in animalsDescription" :key="item.id">
        
      </div>

    </div> -->


  </div>
</template>

<script>
import animals from './animals.json'
import MainAnimalCard from "@/views/OptionsMenu/Tabs/ShopTab/AnimalModal/commons/MainAnimalCard";
import AnimalCard from "@/views/OptionsMenu/Tabs/ShopTab/AnimalModal/commons/AnimalCard";
import AnimalDescription from "@/views/OptionsMenu/Tabs/ShopTab/AnimalModal/commons/AnimalDescription";
export default {
  name: "AnimalModal",
  components: {AnimalDescription, AnimalCard, MainAnimalCard},
  data() {
    return {
      activeEl: null,
      animalsList: animals,
      // animalsDescription: [{id:1, name:'Cyril', },
      //   {id:2, name:'Angelica',}]
    }
  },
  methods: {
    animalModalClick(id) {
        if (this.activeEl == null) {
          console.log('asd')
          document.getElementById(id).classList.add('active')
          this.activeEl = id
        } else {
          document.getElementById(this.activeEl).classList.remove('active')
          this.activeEl = id
          document.getElementById(this.activeEl).classList.add('active')
        }
    }
  }

}


// {
//      "id":10,
//      "name":"Горная львица",
//      "buyName":"lion",
//      "color":"#FC9B41",
//      "opacity":"0.25",
//      "image":"/img/Shop/animal-lion.png",
//      "left":"5.52rem",
//      "width":"10.833rem",
//      "height":"auto",
//      "price":10000,
//      "abilities": ["Убивает с 1 укуса", "Нападает на животных", "Нападает на людей"],
//      "description":"Имеет 100 HP. Может быть убит только другими животными. Ускоряет выращивание плодов на ферме в два раза (не активно, пока животное мертво)."
//   },
//   {
//      "id":11,
//      "name":"Пума",
//      "buyName":"puma",
//      "color":"#FC9B41",
//      "opacity":"0.25",
//      "image":"/img/Shop/animal-puma.png",
//      "left":"5.52rem",
//      "width":"10.833rem",
//      "height":"auto",
//      "price":10000,
//      "abilities": ["Убивает с 1 укуса", "Нападает на животных", "Нападает на людей"],
//      "description":"Имеет 100 HP. Может быть убит только другими животными. Ускоряет выращивание плодов на ферме в два раза (не активно, пока животное мертво)."
//   }
</script>

<style lang="scss" scoped>
.modal {
  position: absolute;
  display: flex;
  top: -0.09rem;
  left: 0;
  width: 100%;
  height: 100%;
  z-index: 100;
  // background: url("/img/optionsMenu/bg.png"), rgba(0, 0, 0, 0.96);
  background-blend-mode: overlay;
}
// .active {
//   border: 0.0495rem solid #5CFF80 !important;
// }
.object-modal-item {
  //transition:  background-color 0.4s ease;
  margin-right: 3.699rem;

  &:hover {
    background-color: #1E1E1E;

    //transform: scale(1.04);
    //border: 1.112px solid #5CFF80 !important;


  }
}

.list {
  height: 40rem;
  margin-left: .5rem;
  overflow-y: scroll;
  direction: rtl;
  &::-webkit-scrollbar {
    width: 0.099rem;
  }

  &::-webkit-scrollbar-track {
    background: linear-gradient(180deg, rgba(255, 255, 255, 0.25) 0%, rgba(255, 255, 255, 0) 100%);
  }

  &::-webkit-scrollbar-thumb {
    background: #5CFF80;
    width: 0.2rem;
  }
  .canvas {
    direction: ltr;
    margin-left: 3rem;
  }
}
.item {
  .wrap {
    display: flex;
    gap: 1rem;
  }
}
.animal-card-modal {
 // transition: border 0.4s ease;
  // &:hover {
  //   border: 0.0495rem solid #5CFF80 !important;
  // }
}
.animal-right-card {
  margin-bottom: 1.2492rem;
}
</style>