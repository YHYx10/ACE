<template>
  <div class="spawn">
    <div
      v-if="activeCharacter && !activeCharacter.ban && activeCharacter.spawnPoints"
      class="spawn-map"
      :style="{ width: `${mapWidth}px`, height: `${mapHeight}px` }"
    >
      <div
        class="spawn-map__point"
        v-for="point in activeCharacter.spawnPoints" 
        :key="point.key"
        :style="[getCoords(point.x, point.y), getZindex(point)]"
        :class="getFreeSpace(point.x, point.y)"
        @click="setActivePoint(point)"
      >
        <img v-if="activePoint != activeCharacter.spawnPoints.indexOf(point)" src="./Assets/Images/Spawn-Point1.svg" alt="Point" /> 
        <img v-else src="./Assets/Images/Spawn-Point.svg" alt="Point" />
        <div v-if="activePoint == activeCharacter.spawnPoints.indexOf(point)" class="spawn-map__card">
          <div class="spawn-map__card-main">
            <div class="spawn-map__card-content">
              <p>{{point.name}}</p>
              <p>{{point.subname}}</p>
            </div>
          </div>
          <button class="spawn-map__card-button" @click="spawnCharacter(point)">
            <p>Choose</p>
          </button>
        </div>
      </div>
    </div>
    <div class="spawn-main">
      <div class="spawn-title">
        <p>Select the point where you want to spawn</p>
        <p>Select the point where you want to appear in the game</p>
      </div>
      <div class="spawn-character">
        <div class="spawn-character__title">
          <p>The choice of character</p>
          <p>Select the character you want to play</p>
        </div>
        <div class="spawn-character__main">
          <div class="spawn-menu">
            <div class="spawn-menu__container">
              <div
                class="spawn-menu__item"
                v-for="(character, idx) in slots"
                :key="(character || {}).name"
                :class="{ active: isCharacterActive(character) }"
                @click="onCharacterAction(character, idx)"
              >
                <div
                  class="spawn-menu__item-inner"
                  :class="[
                    character ? `character${idx + 1}` : '',
                    { add: !character },
                  ]"
                ></div>
              </div>
              <div class="spawn-menu__item" @click="deleteCharacter">
                <div class="spawn-menu__item-inner delete"></div>
              </div>
            </div>
          </div>
          <div class="spawn-card" v-if="activeCharacter">
            <div class="spawn-card__header">
              <div class="spawn-card__header-title">
                <p>{{ activeCharacter.name }}</p>
                <p>Account #{{activeCharacterSlot + 1}}</p>
              </div>
              <div class="spawn-card__blocked" v-if="activeCharacter.ban">
                <p>Blocked</p>
                <div class="spawn-card">
                  <div class="spawn-card__content">
                    <div class="spawn-card__content-item">
                      <p>Blocked to</p>
                      <p>{{ activeCharacter.ban.time}}</p>
                    </div>
                    <div class="spawn-card__content-item">
                      <p>Cause</p>
                      <p>{{ activeCharacter.ban.reason}}</p>
                    </div>
                    <div class="spawn-card__content-item">
                      <p>Administrator</p>
                      <p>{{ activeCharacter.ban.admin}}</p>
                    </div>
                  </div>
                </div>
              </div>
              <div class="spawn-card__points" v-else>
                <div class="spawn-card__points-item" 
                  v-for="spawn in activeCharacter.spawnPoints" 
                  :key="spawn.key"
                  :class="spawn.key"
                  @click="spawnCharacter(spawn)"
                >
                  <div>
                    <span>{{spawn.spawnType === 0 ? 'Family' : 
                            spawn.spawnType === 1 ? 'Place of exit' :
                            spawn.spawnType === 2 ? 'House' : 'Organization'}}
                    </span>
                  </div>
                  <component :is='getIcon(spawn.key)'></component>
                </div>
              </div>
            </div>
            <div class="spawn-card__content">
              <div class="spawn-card__content-item">
                <p>Level</p>
                <p>{{activeCharacter.level}}</p>
              </div>
              <div class="spawn-card__content-item">
                <p>Organization</p>
                <p>{{activeCharacter.frac}}</p>
              </div>
              <div class="spawn-card__content-item">
                <p>Cash</p>
                <p>${{activeCharacter.cash.toLocaleString("de-DE")}}</p>
              </div>
              <div class="spawn-card__content-item">
                <p>Money on the bench</p>
                <p>${{activeCharacter.bank.toLocaleString("de-DE")}}</p>
              </div>
              <div class="spawn-card__content-item">
                <p> health</p>
                <p>{{activeCharacter.hp}} HP</p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapState } from 'vuex';
import Spawn1 from "./Assets/Icons/Spawn1.vue";
import Spawn2 from "./Assets/Icons/Spawn2.vue";
import Spawn3 from "./Assets/Icons/Spawn3.vue";
import Spawn4 from "./Assets/Icons/Spawn4.vue";
export default {
  comments: {
    Spawn1,
    Spawn2,
    Spawn3,
    Spawn4
  },
  data() {
    return {
      mapWidth: 0,
      mapHeight: 0,
      activeCharacter: null,
      activePoint: null,
    };
  },
  computed: {
    ...mapState('characterSelect', ['slots']),   
    activeCharacterSlot() {
      return this.activeCharacter ? this.slots.indexOf(this.activeCharacter): -1;
    }
  },
  methods: {
    setMapStyles() {
      let { innerWidth, innerHeight } = window;
      if ((innerWidth / 16) * 9 < innerHeight) {
        [this.mapHeight, this.mapWidth] = [(innerWidth / 16) * 9, innerWidth];
      } else {
        [this.mapHeight, this.mapWidth] = [innerHeight, (innerHeight / 9) * 16];
      }
    },
    getCoords(x, y) {
      return {
        left: `${0.402 * this.mapWidth + (y / 14500) * this.mapWidth}px`,
        top: `${0.563 * this.mapHeight - (x / 8100) * this.mapHeight}px`,
      };
    },
    getZindex(point) {
      if(this.activePoint == this.activeCharacter.spawnPoints.indexOf(point)){
        return {
          "z-index": 1
        };
      }else{
        return {
          "z-index": 2
        };
      }
      
    },
    getFreeSpace(x, y) {
      let left = 0.402 * this.mapWidth + (y / 14500) * this.mapWidth;
      let top = 0.563 * this.mapHeight - (x / 8100) * this.mapHeight;
      let imageSize = (window.innerHeight / 100) * 10.37;
      let cardSize = {
        width: (window.innerHeight / 100) * (31.667 + 1.111),
        height: (window.innerHeight / 100) * (6.667 + 1.111),
      };
      let pointClass =
        left + imageSize / 2 + cardSize.width <= this.mapWidth
          ? "right"
          : left - imageSize / 2 - cardSize.width >= this.mapWidth
          ? "left"
          : top - imageSize / 2 - cardSize.height >= 0
          ? "top"
          : top + imageSize / 2 + cardSize.height <= this.mapHeight
          ? "bottom"
          : "right";
      return pointClass;
    },
    getIcon(key) {
      const Icons = {
        "s1": Spawn4,
        "s2": Spawn2,
        "s3": Spawn1,
        "s4": Spawn3,
      }
      return Icons[key];
    },
    isCharacterActive(character) {
      return character && this.activeCharacter == character;
    },
    onCharacterAction(character, idx) {
      if (character) {
        if (!this.isCharacterActive(character)){
          this.activeCharacter = character;
          window.mp.triggerServer('auth:char:select', this.activeCharacterSlot)
        }
      } else {
        window.mp.triggerServer('auth:char:select', idx)
      }
    },
    deleteCharacter() {
      if(this.activeCharacterSlot != -1){
        window.mp.triggerServer('auth:char:delete', this.activeCharacterSlot)
      }
    },
    spawnCharacter(spawn) {
      window.mp.triggerServer('auth:char:spawn', spawn.spawnType)
    },
    setActivePoint(point){
      this.activePoint = this.activeCharacter.spawnPoints.indexOf(point)
    }
  },
  mounted() {
    window.addEventListener("resize", this.setMapStyles);
    this.setMapStyles();
  },
};
</script>

<style lang="scss" src="./Spawn.scss"></style>