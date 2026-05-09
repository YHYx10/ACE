<template>
  <div>
    <div class="title">
      <span>Color - </span>
      <span class="value">{{ currentIndex }}</span>
    </div>
    <div class="colors">
      <div 
        v-for="i in currentColors" 
        :key="i" class="color" 
        @click="currentIndex = i"
        :class="{active: i === currentIndex}"
        :style="{background: getColor(i)}"
      >

      </div>
    </div>
  </div>
</template>

<script>
import {mapGetters} from 'vuex'

export default {
  name: 'SelectColor',

  data: function() {
    return {
      colorsList: [
        { id: 0, code: '#1c1f21' },
        { id: 1, code: '#272a2c' },
        { id: 2, code: '#312e2c' },
        { id: 3, code: '#35261c' },
        { id: 4, code: '#4b321f' },
        { id: 5, code: '#5c3b24' },
        { id: 6, code: '#6d4c35' },
        { id: 7, code: '#6b503b' },
        { id: 8, code: '#765c45' },
        { id: 9, code: '#7f684e' },
        { id: 10, code: '#99815d' },
        { id: 11, code: '#a79369' },
        { id: 12, code: '#af9c70' },
        { id: 13, code: '#bba063' },
        { id: 14, code: '#d6b97b' },
        { id: 15, code: '#dac38e' },
        { id: 16, code: '#9f7f59' },
        { id: 17, code: '#845039' },
        { id: 18, code: '#682b1f' },
        { id: 19, code: '#61120c' },
        { id: 20, code: '#640f0a' },
        { id: 21, code: '#7c140f' },
        { id: 22, code: '#a02e19' },
        { id: 23, code: '#b64b28' },
        { id: 24, code: '#a2502f' },
        { id: 25, code: '#aa4e2b' },
        { id: 26, code: '#626262' },
        { id: 27, code: '#808080' },
        { id: 28, code: '#aaaaaa' },
        { id: 29, code: '#c5c5c5' },
        { id: 30, code: '#463955' },
        { id: 31, code: '#5a3f6b' },
        { id: 32, code: '#763c76' },
        { id: 33, code: '#ed74e3' },
        { id: 34, code: '#eb4b93' },
        { id: 35, code: '#f299bc' },
        { id: 36, code: '#04959e' },
        { id: 37, code: '#025f86' },
        { id: 38, code: '#023974' },
        { id: 39, code: '#3fa16a' },
        { id: 40, code: '#217c61' },
        { id: 41, code: '#185c55' },
        { id: 42, code: '#b6c034' },
        { id: 43, code: '#70a90b' },
        { id: 44, code: '#439d13' },
        { id: 45, code: '#dcb857' },
        { id: 46, code: '#e5b103' },
        { id: 47, code: '#e69102' },
        { id: 48, code: '#f28831' },
        { id: 49, code: '#fb8057' },
        { id: 50, code: '#e28b58' },
        { id: 51, code: '#d1593c' },
        { id: 52, code: '#ce3120' },
        { id: 53, code: '#ad0903' },
        { id: 54, code: '#880302' },
        { id: 55, code: '#1f1814' },
        { id: 56, code: '#291f19' },
        { id: 57, code: '#2e221b' },
        { id: 58, code: '#37291e' },
        { id: 59, code: '#2e2218' },
        { id: 60, code: '#231b15' },
        { id: 61, code: '#020202' },
        { id: 62, code: '#706c66' },
        { id: 63, code: '#9d7a50' },
      ],
    }
  },

  props: {
    currentColors: Number,
    value: Object
  },

  computed: {
    ...mapGetters('localization', ['loc']),
    currentIndex: {
      get: function() {
        return this.value.color
      },
      set: function(value) {
        this.value.color = value;
        this.$emit("onChange");
      }
    }
  },
  methods: {
    getColor(key) {
      //console.log(this.colorsList[key].code)
      return this.colorsList[key].code;
    },
  },
}
</script>

<style lang="scss" scoped>

.colors {
  margin-top: 1.574vh;
  padding: 0.53vh 0;
  padding-left: 0.53vh;
  display: flex;
  justify-content: left;
  flex-wrap: wrap;
  gap: 2.87vh;
  width: 30.925vh;
  height: 24.351vh;
  overflow-y: scroll;
  &::-webkit-scrollbar {
    display: none;
  }
  .color {
    width: 3.70vh;
    height: 3.70vh;
    background: white;
    border-radius: 100%;
    outline: rgba(255, 255, 255, 0.29) solid 0.089vw;
    outline-offset: 0.305vh;
    &.active {
      outline-color: #EA0505;
    }
  }
}
.title {
  margin-top: 1.48vh;
  font-family: 'Montserrat';
  font-weight: 600;
  font-size: 1.67vh;
  line-height:  2.037vh;
  color: #ffffff;
  .value{
    background: linear-gradient(180deg, #301934  0%, #EA0505 49.48%, #301934  100%);
    -webkit-background-clip: text;
    -webkit-text-fill-color: transparent;
    background-clip: text;
  }
}
</style>
