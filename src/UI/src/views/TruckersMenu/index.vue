<template>
  <div class="truckers-menu">
    <div class="truckers-menu_welcome">
      <div class="truckers-menu_arrow">
        <img
          src="/img/truckersMenu/arrow-bg.svg"
          alt=""
          v-for="elem in 3"
          :key="elem"
        />
      </div>

      <img
        src="/img/truckersMenu/truck.png"
        alt=""
        class="truckers-menu_truck"
      />
      <div class="truckers-menu_welcome-top">
        <div class="truckers-menu_title">Hello</div>
        <div class="truckers-menu_name">{{ statistics.username }}</div>
        <div class="about">
       Draw orders for businesses is possible to trucks starting from level 4.
        </div>
      </div>
      <div class="truckers-menu_welcome-bottom">
        <div class="truckers-menu_lvl">
          <img src="/img/truckersMenu/exp-bg.png" alt="" />
          <span>{{ truckersData.level }}</span>
        </div>

        <div class="truckers-menu_descr">The level of skill</div>

        <div class="truckers-menu_dots">
          <div
            v-for="elem in 15"
            :key="elem"
            :class="{
              active:
                (15 / 100) *
                  ((truckersData.currentExp / truckersData.requiredExp) *
                    100) >=
                elem,
            }"
          ></div>
        </div>

        <div class="truckers-menu_descr"> to the next level</div>

        <div class="truckers-menu_exp">
          {{ truckersData.currentExp }}&nbsp;/&nbsp;{{
            truckersData.requiredExp
          }}
        </div>
      </div>
    </div>

    <div class="truckers-menu_scroll">
      <div class="truckers-menu_list">
        <div
          class="truckers-menu_item"
          v-for="(elem, index) in truckersData.trucks"
          :key="index"
        >
          <div class="truckers-menu_lock" v-if="!elem.available">
            <img src="/img/truckersMenu/lock.png" alt="" />
            <div>Available with</div>
            <div>{{ elem.availableLevel }} Seed.</div>
          </div>

          <div class="truckers-menu_info">
            <div>{{ elem.name }}</div>
            <!-- flex justify content space-between -->
            <img
              :src="`/img/truckersMenu/items/${elem.name}.png`"
              alt=""
            /><!-- absolute -->
            <div class="truckers-menu_prices">
              <div>
                <div
                  v-for="(item, idx) in [elem.rentCost, elem.payment]"
                  :key="idx + '1'"
                  class="truckers-menu_cost"
                >
                  <div>{{ idx === 0 ? "Lease in an hour" : "Price for the order " }}</div>
                  <div>${{ item }}</div>
                </div>
              </div>
              <DefaultBtn
                class="truckers-menu_rent"
                @click="setTruck(elem.id)"
                v-if="elem.available"
              >
                <span>Rent</span>
              </DefaultBtn>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- <div class="truckers-menu__title">{{truckersData.rankName}}</div>
    <div class="truckers-menu__level-state">
      <div class="icon">
        <div class="value">{{truckersData.level}}</div>
      </div>
      <div class="state-info">
        <div class="progress-wrap">
          <div class="progress-back"></div>
          <div class="progress-line" v-bind:style="{width: currentProgressBar}"></div>
        </div>
        <div class="desc">{{loc('TruckersMenu_1')}} <span>{{truckersData.currentExp}}</span> / <span>{{truckersData.requiredExp}}</span></div>
      </div>
    </div>
    <div class="truckers-menu__cars">
      <div
        v-for="item in this.truckersData.trucks"
        :key="item.key"
        :class="[{available: item.available}, 'truckers-menu__cars-item']"
      >
        <div class="info">
          <div class="info__name">{{item.name}}</div>
          <div class="info__cost">
            <div class="value">${{item.rentCost}}</div>
            <span>{{loc('TruckersMenu_2')}}</span>
          </div>
          <div class="info__cost">
            <div class="value">${{item.payment}}</div>
            <span>{{loc('TruckersMenu_7')}}</span>
          </div>
        </div>
        <div class="info-img" :style="{backgroundImage: 'url(/img/truckersMenu/items/trucker-car-' + item.id + '.png)'}"></div>
        <div
          v-if="item.available" 
          class="btn-rent"
          @click="setTruck(item.id)"
        >{{loc('TruckersMenu_3')}}</div>
        <div 
          v-else 
          class="unavailable"
        >
          <div class="info info-level">
            <div class="desc">{{loc('TruckersMenu_4')}}</div>
            <div class="value">{{item.availableLevel}} {{loc('TruckersMenu_5')}}</div>
          </div>
          <div class="info info-reward" v-if="item.reward">
            <div class="desc">{{loc('TruckersMenu_6')}}</div>
            <div class="value">+{{item.reward}}</div>
          </div>
        </div>
      </div>
    </div> -->
  </div>
</template>

<script>
import DefaultBtn from '../UI/button/DefaultBtn.vue'
import { mapGetters, mapState } from "vuex";
export default {
  name: "TruckersMenu",

  computed: {
    ...mapState("truckersMenu", ["truckersData"]),
    ...mapState("optionsMenu", ["statistics"]),
    ...mapGetters("localization", ["loc"]),
    currentProgressBar: function () {
      let currentValue = this.truckersData.currentExp;
      let maxValue = this.truckersData.requiredExp;
      let percent = (currentValue / maxValue) * 100;
      return percent + "%";
    },
  },
  components: {
    DefaultBtn
  },
  methods: {
    setTruck: function (id) {
      window.mp.trigger("truckersMenu:setTruck", id);
    },
  },
};
</script>

<style lang="scss" scoped>
$width: 1920;
$height: 1080;

@function conv($px, $direction: false) {
  @if $direction {
    @return ($px / $height) * 100vh;
  } @else {
    @return ($px / $width) * 100vw;
  }
}

.truckers-menu {
  width: 100%;
  height: 100vh;
  overflow: hidden;
  display: grid;
  grid-template-columns: 1fr conv(788);
  column-gap: conv(14.68);
  background: rgba(0, 0, 0, 0.98);
  position: relative;
  color: white;
  font-family: "Akrobat";

  &,
  span,
  button {
    font-family: "Akrobat";
  }

  /* &::after {
    content: "";
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    border-radius: 50%;
    width: conv(1917, true);
    height: conv(1917, true);
    background: rgba(255, 255, 255, 0.2);
    opacity: 0.25;
    filter: blur(250px);
    pointer-events: none;
    z-index: 1;
  } */

  & > div {
    z-index: 5;
  }

  &_arrow,
  &_truck {
    position: absolute;
    pointer-events: none;
  }

  &_arrow {
    top: 50%;
    height: conv(998, true);
    left: 50%;
    transform: translate(-50%, -50%);
    z-index: 2;

    img {
      position: absolute;
      left: 50%;
      transform: translateX(-50%);
      height: conv(493, true);

      &:first-child {
        top: 0;
      }

      &:nth-child(2) {
        top: 50%;
        transform: translate(-50%, -50%);
      }

      &:nth-child(3) {
        bottom: 0;
      }
    }
  }

  &_truck {
    right: 0;
    width: auto;
    height: conv(565, true);
    top: conv(272, true);
    z-index: 3;
  }

  &_welcome {
    width: 100%;
    height: 100vh;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: space-between;
    position: relative;
    padding: conv(105, true) 0 conv(93, true);

    & > div {
      display: flex;
      flex-direction: column;
      align-items: center;
    }

    &-top div {
      font-weight: 700;
      text-align: center;
      text-transform: uppercase;
    }
  }

  &_title {
    font-size: conv(48, true);
    line-height: conv(58, true);
    color: #ffcc47;
    margin-bottom: conv(9, true);
  }

  &_name {
    font-size: conv(32, true);
    line-height: conv(38, true);
  }
  .about {
    margin-top: 1vh;
    font-size: 1.296vh;
    width: 50%;
    color: rgba(255, 255, 255, 0.5);
  }

  &_lvl {
    height: conv(73, true);
    width: auto;
    margin-bottom: conv(17, true);
    position: relative;

    img,
    span {
      position: absolute;
      top: 50%;
      left: 50%;
      transform: translate(-50%, -50%);
    }

    img {
      height: 100%;
      width: auto;
    }

    span {
      font-weight: 700;
      font-size: conv(32, true);
      line-height: conv(38, true);
      text-align: center;
      text-transform: uppercase;
      color: #ffffff;
      top: 45%;
    }
  }

  &_descr {
    font-weight: 700;
    text-align: center;
    text-transform: uppercase;
    color: #ffffff;

    &:nth-child(2) {
      font-size: conv(20, true);
      line-height: conv(24, true);
    }

    &:nth-child(4) {
      font-size: conv(16, true);
      line-height: conv(19, true);
    }
  }

  &_dots {
    display: flex;
    justify-content: center;
    align-items: center;
    margin: conv(30, true) 0 conv(28, true);

    div {
      width: conv(17, true);
      height: conv(17, true);
      background: rgba(255, 255, 255, 0.05);
      border-radius: 50%;

      &:not(:last-child) {
        margin-right: conv(6.43, true);
      }

      &.active {
        background: #301934 ;
      }
    }
  }

  &_exp {
    margin-top: conv(10, true);
    font-weight: 700;
    font-size: conv(20, true);
    line-height: conv(24, true);
    text-align: center;
    text-transform: uppercase;
    color: #ffcc47;
  }

  &_scroll {
    padding: conv(78, true) conv(83) conv(79, true) 0;
    display: flex;
    height: 100vh;
  }

  &_list {
    display: grid;
    grid-template-columns: 1fr 1fr;
    row-gap: conv(10, true);
    column-gap: conv(10);
    padding-right: conv(10);
    overflow-y: scroll;
    overflow-x: hidden;
    max-height: 100%;
    height: 100%;
    width: 100%;

    &::-webkit-scrollbar {
      width: conv(5);
    }

    &::-webkit-scrollbar-track {
      background: rgba(255, 255, 255, 0.05);
    }

    &::-webkit-scrollbar-thumb {
      background: #301934 ;
    }
    //max-height: conv(923, true);
  }

  &_item {
    width: 100%;
    height: conv(301, true);
    min-height: conv(301, true);
    position: relative;
  }

  &_info {
    width: 100%;
    height: 100%;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: space-between;
    padding: conv(23, true) conv(60) conv(17, true);
    background: rgba(255, 255, 255, 0.03);
    // transition: 0.2s ease;

    &:hover {
      background: rgba(255, 255, 255, 0.07);

      .truckers-menu_rent {
        display: flex;
      }
      .truckers-menu_cost{
        opacity: 0;
      }
    }

    & > div:first-child {
      font-weight: 700;
      font-size: conv(32, true);
      line-height: conv(38, true);
      text-align: center;
      text-transform: uppercase;
    }

    & > img {
      position: absolute;
      top: conv(82, true);
      left: 50%;
      transform: translateX(-50%);
      height: conv(130, true);
      width: auto;
      z-index: -1;
    }
  }

  &_prices {
    width: 100%;
    position: relative;

    & > div {
      display: grid;
      grid-template-columns: 1fr 1fr;
      justify-items: space-between;
    }
  }

  &_rent {
    cursor: pointer;
    position: absolute;
    display: none;
    height: conv(75, true);
    width: conv(310);
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    font-weight: 700;
    font-size: conv(24, true);
    line-height: conv(29, true);
    text-align: center;
    text-transform: uppercase;
    color: #ffffff;
    z-index: 5;
    span {
      z-index: 7;
    }
  }

  &_cost {
    white-space: nowrap;
    display: flex;
    flex-direction: column;
    align-items: center;

    div {
      text-align: center;
      text-transform: uppercase;
      font-weight: 700;

      &:first-child {
        font-size: conv(12, true);
        line-height: conv(14, true);
        color: rgba(255, 255, 255, 0.55);
        margin-bottom: conv(3, true);
      }

      &:last-child {
        font-size: conv(32, true);
        line-height: conv(38, true);
        color: #a0ff98;
      }
    }
  }

  &_lock {
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    background: rgba(0, 0, 0, 0.95);
    z-index: 10;

    img {
      width: conv(80, true);
      height: conv(80, true);
      margin-bottom: conv(17, true);
    }

    div {
      font-weight: 700;
      text-align: center;
      text-transform: uppercase;

      &:nth-child(2) {
        font-size: conv(20, true);
        line-height: conv(24, true);
        color: #ffffff;
        margin-bottom: conv(4, true);
      }

      &:last-child {
        font-size: conv(32, true);
        line-height: conv(38, true);
        color: #301934 ;
      }
    }
  }
}
/* .truckers-menu{
  display: flex;
  flex-flow: column;
  width: 100%;
  height: 100%;
  position: absolute;
  top: 0;
  left: 0;
  text-transform: uppercase;
  color: #fff;
  text-transform: uppercase;
  background-image: url('/img/truckersMenu/bg.png');
  background-size: cover;
  padding: 2.8rem 0 2.8rem 2.6rem;
  &__title{
    font-size: 2.4rem;
    line-height: 2.9rem;
    margin-bottom: 1rem;
  }
  &__level-state{
    display: flex;
    align-items: center;
    .icon{
      margin-right: 1.25rem;
      width: 1.75rem;
      height: 2.5rem;
      background-image: url('/img/truckersMenu/level-icon.svg');
      background-size: contain;
      background-position: center;
      background-repeat: no-repeat;
      padding-top: .25rem;
      display: flex;
      align-items: flex-start;
      justify-content: center;
      .value{
        font-weight: bold;
        font-size: 1.55rem;
        line-height: 1.55rem;
        letter-spacing: 0.05em;
        color: #2A3202;
      }
    }
    .state-info{
      display: flex;
      flex-flow: column;
      align-items: center;
      .progress-wrap{
        margin-bottom: .8rem;
        display: flex;
        align-items: center;
        justify-content: flex-start;
        width: 12.5rem;
        height: .5rem;
        position: relative;
        .progress-back, .progress-line{
          height: 100%;
          position: absolute;
          top: 0;
          left: 0;
          overflow: hidden;
        }
        .progress-back{
          border: 1px solid rgba(255, 255, 255, 0.2);
          width: 100%;
        }
        .progress-line{
          background: linear-gradient(270deg, #B6D300 0%, rgba(182, 211, 0, 0) 100%);
          display: flex;
          align-items: center;
          justify-content: center;
          &:after{
            content: '';
            position: absolute;
            right: 0;
            width: 2.25rem;
            height: .9rem;
            background: radial-gradient(103.64% 63.89% at 81.11% 50%, #FFFFFF 0%, rgba(255, 255, 255, 0) 74.48%);
          }
        }
      }
      .desc{
        font-size: .9rem;
        line-height: 1.1rem;
        letter-spacing: 0.15em;
        color: #FFFFFF;

      }
    }
  }
  &__cars{
    display: grid;
    grid-template-columns: repeat(3, 1fr);
    padding-right: 1.75rem;
    grid-auto-rows: 10rem;
    margin-top: 2.5rem;
    grid-row-gap: .8rem;
    grid-column-gap: 1.2rem;
    overflow-y: auto;
    padding-bottom: 1.75rem;
    margin-bottom: -1.75rem;
    margin-right: .5rem;
    &::-webkit-scrollbar {
      width: .3rem;
    }
    &::-webkit-scrollbar-track {
      background: radial-gradient(50% 50% at 50% 50%, rgba(255, 255, 255, 0.65) 0%, rgba(255, 255, 255, 0) 100%);
      width: 1px;
    }
    &::-webkit-scrollbar-thumb{
      background: #E5E5E5;
      border-radius: 1rem;
      width: 1px;
    }
    &-item{
      display: flex;
      align-items: flex-start;
      justify-content: space-between;
      padding: 1.5rem 2rem;
      background: rgba(0, 0, 0, 0.45);
      border: 1px solid rgba(255, 255, 255, 0.15);
      box-shadow: 0 .6rem 1.1rem rgba(0, 0, 0, 0.35);
      border-radius: .5rem;
      position: relative;
      .btn-rent{
        display: flex;
        position: absolute;
        opacity: 0;
        left: 50%;
        transform: translateX(-50%);
        align-self: center;
        align-items: center;
        justify-content: center;
        background: rgba(193, 231, 4, 0.15);
        border: 1px solid #C1E704;
        border-radius: .5rem;
        transition: .2s;
        z-index: 2;
        padding: 1rem 2.3rem;
        font-weight: bold;
        font-size: 1.05rem;
        letter-spacing: 0.05em;
        color: #FFFFFF;
        &:hover{
          background: rgba(193, 231, 4, 0.35);
          transition: .2s;
        }
      }
      .unavailable{
        width: 100%;
        height: 100%;
        background: radial-gradient(50% 50% at 50% 50%, rgba(0, 0, 0, 0) 50.52%, #000000 100%), rgba(0, 0, 0, 0.75);
        border: 1px solid rgba(255, 255, 255, 0.15);
        box-sizing: border-box;
        border-radius: .5rem;
        position: absolute;
        top: 0;
        left: 0;
        display: flex;
        align-items: center;
        justify-content: center;
        white-space: pre;
        font-size: 1.2rem;
        line-height: 1.95rem;
        letter-spacing: 0.05em;
        color: #FFFFFF;
        .info{
          display: flex;
          flex-flow: column;
          align-items: flex-start;
          justify-content: center;
          margin-right: 1.4rem;
          &:last-child{
            margin-right: 0;
          }
          .desc{
            font-size: .9rem;
          }
          .value{
            font-size: 1.75rem;
            position: relative;
            display: flex;
            align-items: center;
            justify-content: center;
            &:before{
              content: '';
              background-size: contain;
              background-position: center;
              background-repeat: no-repeat;
              position: absolute;
              height: 1.3rem;
              margin-bottom: .2rem;
            }
          }
          &.info-level{
            .value{
              color: rgba(240,44,44,1);
              padding-left: 1.5rem;
              &:before{
                margin-right: .9rem;
                width: .95rem;
                background-image: url('/img/truckersMenu/locked.svg');
                left: 0;
              }
            }
          }
          &.info-reward{
            padding-left: 1.5rem;
            position: relative;
            &:before{
              position: absolute;
              content: '';
              width: 1px;
              height: 4rem;
              background: rgba(255, 255, 255, 0.15);
              left: 0;
            }
            .value{
              color: rgba(255,242,5,1);
              &:before{
                width: 1.3rem;
                background-image: url('/img/truckersMenu/forward.svg');
                right: 0;
                transform: translateX(1.8rem);
              }
            }
          }
        }
      }
      &.available{
        &:hover{
          .btn-rent{
            opacity: 1;
            transition: .3s;
          }
          &:after{
            content: '';
            z-index: 1;
            width: 100%;
            height: 100%;
            position: absolute;
            top: 0;
            left: 0;
            background: rgba(0, 0, 0, 0.65);
            border: .1rem solid #C1E704;
            box-sizing: border-box;
            box-shadow: 0 .6rem 1.1rem rgba(0, 0, 0, 0.35);
            border-radius: .5rem;
            transition: .3s;
          }
        }
      }
      .info{
        display: flex;
        flex-flow: column;
        height: 100%;
        justify-content: space-between;
        &__name{
          font-size: 1.5rem;
          line-height: 1.8rem;
          letter-spacing: 0.05em;
        }
        &__cost{
          font-size: 1.3rem;
          line-height: 1.3rem;
          letter-spacing: 0.05em;
          color: #C1E704;
          display: flex;
          align-items: center;
          span{
            font-family: BPG Arial Caps;
            font-size: .8rem;
            line-height: .95rem;
            color: rgba(255, 255, 255, 0.65);
            margin-left: .4rem;
            text-transform: lowercase;
          }
        }
      }
      .info-img{
        width: 9.4rem;
        height: 6.45rem;
        background-size: contain;
        background-position: center;
        background-repeat: no-repeat;
      }
    }
  }
} */
</style>
