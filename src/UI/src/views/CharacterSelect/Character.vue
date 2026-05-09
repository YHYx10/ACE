<template>
<div class="select_person__item select_person__item-active">
            <div class="select_person__content">

              <div class="select_person__data">
                <div class="select_person__information">
                  <h3>Character #1</h3>
                  <h4>{{item.name.replace('_', ' ')}}</h4>
                </div>
                <div class="select_person__image">
                  <img src="/img/characterSelect/avatar-person.png" alt="Avatar Person" class="select_person__img">
                </div>
              </div>
              <div v-if="item.ban" class="select_person__slot">
                <h3>The character is blocked</h3>
                <h4>administrator <b style="background: linear-gradient(89.71deg, #301934  0.25%, #591b87 99.8%);-webkit-background-clip: text;-webkit-text-fill-color: transparent;">{{item.ban.Administrator}}</b> Blocked this character for: <b style="background: linear-gradient(89.71deg, #301934  0.25%, #591b87 99.8%);-webkit-background-clip: text;-webkit-text-fill-color: transparent;">{{item.ban.Reason}}</b></h4>
                <br>
                <h4>Blocking date: {{parseTime(item.ban.BannedAt)}}<br>Will be available: {{parseTime(item.ban.BannedUntil)}}</h4>
              </div>
              <div v-else class="select_person__body">
                <h3 class="select_person__description">
                Character Statistics
                </h3>

                <div class="select_person__stats">
                  <h3 class="select_person__label">
               Character level
                  </h3>
                  <h4 class="select_person__base">
                    {{item.level}}
                  </h4>
                </div>

                <div class="select_person__stats">
                  <h3 class="select_person__label">
                  Half of the character
                  </h3>
                  <h4 class="select_person__base">
                    {{item.gender == 1 ? "Male" : "Female"}}
                  </h4>
                </div>

                <div class="select_person__stats">
                  <h3 class="select_person__label">
                    {{loc('characterSelect_9')}}
                  </h3>
                  <h4 class="select_person__base">
                    {{item.frac}}
                  </h4>
                </div>

                <div class="select_person__stats">
                  <h3 class="select_person__label">
                    {{loc('characterSelect_10')}}
                  </h3>
                  <h4 class="select_person__base">
                    ${{item.bank}}
                  </h4>
                </div>

                <div class="select_person__stats">
                  <h3 class="select_person__label">
                    {{loc('characterSelect_11')}}
                  </h3>
                  <h4 class="select_person__base">
                    ${{item.cash}}
                  </h4>
                </div>

              </div>


            </div>

              <button 
                v-if="!item.ban" 
                @click="chooseCharacter"
                class="btn btn--active"
              >{{loc('characterSelect_12')}}</button>
              <!-- <button 
                v-if="!item.ban" 
                @click="deleteCharacter"
                class="btn btn--active"
              >{{loc('characterSelect_13')}}</button> -->

          </div>
</template>

<script>
import { mapGetters } from 'vuex'

export default {
  props:{
    index: Number,
    item: Object,
    setModal: Function
  },
  data() {
    return {
      lastClick: 0,
      floodTime: 1500
    }
  },
  computed: {
    ...mapGetters('localization', ['loc']),
  },
  methods: {
    deleteCharacter(){
      if(this.item.level < 10)
        this.setModal(true, 2, this.index);
      else
        this.setModal(true, 1, this.index);
    },
    parseTime(time){
      return new Date(time).toLocaleString('be-BY')
    },

    chooseCharacter: function() {
      if(this.lastClick > Date.now()) return;
      this.lastClick = Date.now() + this.floodTime;
      window.mp.triggerServer('auth:char:select', this.index);
    }
  }
}
</script>



<style lang="scss" scoped>
.select_person {
  padding-top: 2.964240102171138vh;

  &__item {
    position: relative;

    transition: all .3s ease .1s;

    &:hover {
      cursor: pointer;
      margin-bottom: 1.5vh;
      margin-top: -1.5vh;
      //transform: scale(1.125, 1.125);
      border: 0.2554278416347382vh solid #301934 ;
    }

    &-active {
      background: rgba(255, 255, 255, 0.01);
      border: 0.2554278416347382vh solid rgba(255, 255, 255, 0.09);
      border-radius: 1.1494252873563218vh;
      background-image: url('/img/characterSelect/item-blur.png');
      background-repeat: no-repeat;
      background-size: cover;
      height: 84.68675995694295vh;
    }
  }

  &__slot {
    display: flex;
    align-items: center;
    flex-direction: column;
    justify-content: center;
    height: 65.671775vh;

    p {
      font-weight: 600;
      font-size: 1.9157088122605364vh;
      line-height: 104%;
      text-transform: uppercase;
      color: rgba(255, 255, 255, 0.25);
      margin-top: 34%;
    }

    h3 {
      font-weight: 800;
      font-size: 3.0651340996168583vh;
      line-height: 3.1928480204342273vh;
      text-transform: uppercase;
      background: linear-gradient(89.71deg, #301934  0.25%, #591b87 99.8%);
      -webkit-background-clip: text;
      -webkit-text-fill-color: transparent;
      background-clip: text;
      text-fill-color: transparent;
      text-shadow: 0 0 14.303959131545339vh rgba(255, 255, 255, 0.25);
      margin-bottom: 0.2554278416347382vh;
    }

    h4 {
      font-weight: 600;
      font-size: 1.6602809706257982vh;
      line-height: 104%;
      text-transform: uppercase;
      color: rgba(255, 255, 255, 0.25);
      max-width: 24.393358876117496vh;
      text-align: center;
    }
  }

  &__content {
    padding: 3.469987228607918vh 5.236270753512133vh 0 5.236270753512133vh;
  }

  &__data {
    display: flex;
    align-items: center;
    justify-content: space-between;
    margin-bottom: 1.1928480204342273vh;
  }

  &__information {

    h3 {
      font-weight: 600;
      font-size: 1.9157088122605364vh;
      line-height: 104%;
      text-transform: uppercase;
      color: rgba(255, 255, 255, 0.25);
    }

    h4 {
      color: #fff;
      max-width: 15.070242656449553vh;
      font-weight: 800;
      font-size: 3.0651340996168583vh;
      line-height: 3.1928480204342273vh;
      text-transform: uppercase;
    }
  }

  &__image {
    width: 9.067688378033205vh;
    height: 9.067688378033205vh;
    border-radius: 100%;
    background-image: url('/img/characterSelect/avatar-round.png');
    background-repeat: no-repeat;
    background-size: cover;
    background-position: center center;
    padding: 1.40485312899106vh;
  }

  &__img {
    width: 6.257982120051086vh;
    height: 6.257982120051086vh;
  }

  &__description {
    max-width: 13.665389527458492vh;
    font-weight: 600;
    font-size: 1.9157088122605364vh;
    line-height: 104%;
    text-transform: uppercase;
    color: rgba(255, 255, 255, 0.25);
    margin-bottom: 1.5759897828863347vh;
  }

  &__stats {
    border: 0.1277139208173691vh solid rgba(255, 255, 255, 0.1);
    background-image: url('/img/characterSelect/bg-data.png');
    background-repeat: no-repeat;
    background-size: cover;
    background-position: center center;
    font-weight: 600;
    font-size: 1.9157088122605364vh;
    font-family: 'Akrobat';
    line-height: 2.2988505747126435vh;
    text-transform: uppercase;
    color: #fff;
    padding: 2.554278416347382vh 0 2.554278416347382vh 4.21455938697318vh;
    position: relative;
    width: 40.86845466155811vh;

    &:not(:last-child) {
      margin-bottom: 0.877139208173691vh;
    }

    &::after {
      content: '';
      position: absolute;
      width: 3.3205619412515963vh;
      height: 0.2554278416347382vh;
      top: 50%;
      left: 0;
      background: #FFFFFF;
      box-shadow: 0 0 1.7879948914431674vh rgba(255, 255, 255, 0.55);
      transform: rotate(-90deg);
    }
  }

  &__label {
    font-weight: 700;
    font-size: 1.5325670498084292vh;
    line-height: 1.7879948914431674vh;
    text-transform: uppercase;
    color: rgba(#fff, 0.25);
  }

  &__base {
    color: #fff;
    font-weight: 600;
    font-size: 1.9157088122605364vh;
    line-height: 2.2988505747126435vh;
    text-transform: uppercase;
  }

  &__logosm-one {
    position: absolute;
    top: 5.363984674329502vh;
    left: 50%;
    transform: translateX(-50%);

    img {
      width: 15.070242656449553vh;
      height: 15.32567049808429vh;
    }
  }

  &__logosm-two {
    position: absolute;
    bottom: 4.736275565123789vh;
    left: 50%;
    transform: translateX(-50%);

    img {
      width: 15.070242656449553vh;
      height: 15.32567049808429vh;
    }
  }

  &__logolg {
    position: absolute;
    top: 21.32822477650064vh;
    left: 50%;
    transform: translateX(-50%);

    img {
      width: 39.84674329501916vh;
      height: 40.229885057471265vh;
      transform: rotate(90deg);
    }
  }
}

.btn {
  cursor: pointer;
  border: unset;

  &--active {
    font-family: 'Akrobat';
    // background: linear-gradient(180deg, #301934  0%, #591b87 100%);
    box-shadow: inset 0 0 1.9157088122605364vh rgba(75, 0, 130, 0.86);
    color: #fff;
    font-weight: 600;
    font-size: 1.7879948914431674vh;
    line-height: 2.1711366538952745vh;
    padding: 3.1928480204342273vh 0;
    width: 85%;
    text-transform: uppercase;
    margin-left: 3.8vh;
    margin-right: 3.8vh;
    margin-top: 2vh;

    transition: all .3s ease .1s;

    &:hover {
      background: linear-gradient(180deg, #921117 0%, #591b87 100%);
    }
  }

  &--disable {
    font-family: 'Akrobat';
    background: linear-gradient(180deg, rgba(71, 44, 132, 0.25) 0%, rgba(75, 0, 130, 0.25) 100%);
    border: 0.1277139208173691vh solid #301934 ;
    box-shadow: inset 0 0 1.9157088122605364vh rgba(75, 0, 130, 0.86);
    font-weight: 600;
    font-size: 1.7879948914431674vh;
    line-height: 2.1711366538952745vh;
    text-transform: uppercase;
    color: rgba(255, 255, 255, 0.25);
    padding: 3.1928480204342273vh 0;
    width: 85%;
    margin-top: 6.4vh;
    margin-right: 3vh;
    margin-left: 3vh;
    position: relative;
    z-index: 999999999999999;
        
    transition: all .3s ease .1s;

    &:hover {
      color: #fff;
      background: linear-gradient(180deg, #921117 0%, #591b87 100%);
    }
  }
}
</style>
