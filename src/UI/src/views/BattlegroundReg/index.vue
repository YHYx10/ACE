<template>
  <div class="headhunting">
    
    <div class="left-shadow" />
    <div class="common-shadow"></div>
    <div class="wrapper">
      <div class="left-side">
        <div class="main-title">
          <div>Hunting</div>
          <div class="sub">Behind the heads</div> 
        </div>
        <div class="description">
         They go to the island with the 9 rivals.<br> Your job is to stay the last in the hunting area. <br> The winner receives a price of $ 30,000
          <br><br>
    In order to start the "hunting" mode, a minimum number of people is required - 2.
        </div>
        
        <div class="reward">
          <div class="wrap">
            <div class="vertical-line"></div>
            <img src="/img/battlegroundReg/reward.svg" alt="">
          </div>
          <div class="col">
            <div class="title">
            reward
            </div>
            <div class="value">
              30 000 $
            </div>
          </div>
        </div>

        <div class="event-info">
          <div class="item">
            <img src="/img/battlegroundReg/timer.svg" alt="" class="ico timer">
            <div class="col">
              <div class="title">
        The registration ends through:
              </div>
              <div class="value">
                {{ time === -1  ? '05:00': parseTime(time) }}
              </div>
            </div>
          </div>
          <div class="item longer">
            <img src="/img/battlegroundReg/participants.svg" alt="" class="ico participants">
            <div class="col">
              <div class="title">
      The participants are registered:
              </div>
              <div class="value">
                {{peopleCount}} / {{peopleCapacity}}
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="bottom-side">
        <DefaultBtn
          v-if="isReg"
          class="btn btn-gray"
        >{{loc('BattlegroundReg_10')}}</DefaultBtn>
        <DefaultBtn
          v-else
          class="btn"
          @click="setShowModal"
        >{{loc('BattlegroundReg_5')}}</DefaultBtn>
        <div class="line" />
        <div class="state">
          {{secondsUntilStart === -1 ? "" : "Es gibt einen Satz für eine Veranstaltung"}}
        </div>
      </div>
    </div>
    
    <Modal v-if="showModal"/>
  </div>
</template>

<script>
import { mapGetters, mapState } from 'vuex'
import DefaultBtn from '../UI/button/DefaultBtn.vue'
import Modal from './Modal'
export default {
  name: 'BattlegroundReg',

  components: {
    Modal,
    DefaultBtn
  },

  data: function() {
    return {
      showModal: null,
      startTime: Date.now(),
      counter: 0,
      timerId: 0,
    }
  },

  computed: {
    ...mapState('battlegroundReg', ['isReg', 'peopleCount', 'peopleCapacity', 'secondsUntilStart']),
    ...mapGetters('localization', ['loc']),
    time() {
      return this.secondsUntilStart - this.counter
    }
  },

  methods: {
    setShowModal: function() {
      window.mp.trigger('battlegroundReg:registerForBattle')
      this.showModal = true
    },
    parseTime(seconds){
      const parse = (number) => String(number).length < 2 ? '0'+number : number
      return `${parse(Math.floor(seconds/60))}:${parse(Math.floor(seconds%60))}`
    },
    timer() {
      const now = Date.now()
      const ms = 1000 - (now - (this.counter*1000 + this.startTime))
      
      this.counter++
      if(this.startTime + this.secondsUntilStart * 1000 < now) return this.counter--
      this.timerId = setTimeout(() => {
          this.timer()
      }, ms)
    }
  },
  watch: {
    secondsUntilStart(newVal) {
      if (newVal >= 0) {
        this.startTime = Date.now()
        clearTimeout(this.timerId)
        this.counter = 0
        this.timer()
      } else {
        clearTimeout(this.timerId)
        this.counter = 0
      }
    }
  },

  mounted(){
    this.showModal = false
    if(this.secondsUntilStart >= 0) this.timer()
  },

  beforeUnmount(){
    clearTimeout(this.timerId)
  }
  
}
</script>

<style lang="scss" scoped>
@keyframes show{
  from {
    transform: translateX(100vw);
  }
  to { 
    transform: translateX(0);
  }
}

.col {
  display: flex;
  flex-direction: column;
  font-weight: 700;
  .title {
    font-size: 0.7rem;
    line-height: 0.85rem;
    color: #FFFFFF;
  }
  .value {
    font-size: 1.6rem;
    line-height: 1.9rem;
    text-transform: uppercase;
    color: #FFFFFF;
  }
}

.headhunting {
  width: 100%;
  height: 100%;
  background-image: url('/img/battlegroundReg/bg.png');
  background-size: cover;
  text-transform: uppercase;
  display: flex;
  align-items: center;
  justify-content: center;
  .wrapper {
    display: flex;
    flex-direction: column;
    gap: 4rem;
    justify-content: center;
  }
  .common-shadow {
    position: absolute;
    width: 100vw;
    height: 100vh;
    background: rgba(0, 0, 0, 0.3);
    box-shadow: inset 0rem -8.2rem 11.5rem rgba(0, 0, 0, 0.8);
  }
  .left-shadow {
    position: absolute;
    width: 100vw;
    height: 100vh;
    background: linear-gradient(89.97deg, #000000 0.03%, rgba(0, 0, 0, 0) 42.68%);
  }
  .left-side {
    position: relative;
    width: 18rem;
    .main-title {
      font-weight: 800;
      font-size: 7.4rem;
      line-height: 7.4rem;
      background: linear-gradient(89.71deg, #301934  0.25%, #591b87 99.8%);
      -webkit-background-clip: text;
      -webkit-text-fill-color: transparent;
      background-clip: text;
      text-fill-color: transparent;
      text-shadow: 0rem 0rem 5.6296rem rgba(190, 32, 32, 0.25);
      .sub {
        font-size: 3.25rem;
        line-height: 3.25rem;
      }
    }
    .description {
      margin-top: 1.5rem;
      font-weight: 700;
      font-size: 0.8rem;
      line-height: 0.95rem;
      color: #fff;
    }

    .reward {
      display: flex;
      align-items: center;
      margin-top: 5.3rem;
      height: 4.25rem;
      border: 0.05rem solid;
      overflow: hidden;
      border-image-source: linear-gradient(to right, red, green, blue);
      .wrap {
        display: flex;
        align-items: center;
      }

      .vertical-line {
        align-self: flex-start;
        width: 0.1rem;
        height: 1.3rem;
        background: white;
        box-shadow: 0rem 0rem 0.7rem rgba(255, 255, 255, 0.55);
        margin-left: 0.735rem;
      }
      img {
        padding: 0.15rem;
        width: 1.65rem;
        height: 1.65rem;
        margin-left: 0.735rem;
        // box-shadow: 0rem 0rem 3.2rem #FFCD4D;
        filter: drop-shadow(0 0 1.2rem #FFCD4D);
      }
      .col {
        margin-left: 1rem;
      }
    }

    .event-info {
      margin-top: 2.5rem;
      display: flex;
      justify-content: space-between;
      .item {
        display: flex;
        gap: 0.5rem;
        width: 7.35rem;
        height: 4.1rem;
        &.longer {
          width: 8rem;
        }
        img.ico {
          height: min-content;
          &.timer {
            width: 1.25rem;
          }
          &.participants {
            width: 1.6rem;
          }
        }
        .col {
          gap: 0.5rem;
          .title {
            font-weight: 800;
          }
        }
      }
    }
  }
  .bottom-side {
    position: relative;
    display: flex;
    gap: 3.5rem;
    align-items: center;
    .btn {
      width: 17.7rem;
      height: 4.15rem;
      color: #fff;
      &-gray {
        border: 1px solid #777777;
        box-shadow: inset 0px 0px 15px rgba(117, 117, 117, 0.86);
        background: linear-gradient(180deg, rgba(205, 205, 205, 0.25) 0%, rgba(106, 106, 106, 0.25) 100%);
        &:hover {
          box-shadow: inset 0px 0px 15px rgba(117, 117, 117, 0.86);
        }
      }
      
    }
    .line {
      border: 0.05rem solid #DA131F;
      width: 40.8rem;
    }
    .state {
      font-weight: 700;
      font-size: 1.6rem;
      line-height: 1.9rem;
      height: 1.9rem;
      color: #fff;
      min-width: 12.5rem;
    }
  }
}
</style>
