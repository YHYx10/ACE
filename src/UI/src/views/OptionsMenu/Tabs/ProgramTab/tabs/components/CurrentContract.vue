<template>
  <div class="current-contract">
    <div class="current-contract__heading">
      <div class="category">section</div>
      <div class="title">{{contract.Name}}</div>
      <div class="subtitle">{{contract.Desc}}</div>
    </div>
    <div class="current-contract__reward">
      <div class="current-contract__reward-heading">
        <div class="category">This is yours</div>
        <div class="title">reward</div>
      </div>
      <div class="current-contract__reward-list">
        <div 
          class="current-contract__reward-list__item"
          v-for="(item, index) in contract.Rewards"
          :key="index"
        >
          <div class="current-contract__reward-list__item-decorate">
            <img :src="`img/optionsMenu/programTab/${item.Image}.png`">
          </div>
          <div v-if="item.Value" class="current-contract__reward-list__item-value">+{{item.Value}}</div>
        </div>
      </div>
    </div>
    <template v-if="contractState && contractState.InProgress">
      <div class="current-contract__find" v-if="contract.Coords">
        <div class="current-contract__find-heading">
          <div class="category">section</div>
          <div class="title">coordinates</div>
          <div class="subtitle">A brief description for example </div>
        </div>
        <div class="current-contract__find-list">
          <div 
            class="item__btn"
            v-for="(coord, index) in contract.Coords"
            :key="index"
            @click="selectCoordinats(coord)"
          >{{coord.Name}}</div>
        </div>
      </div>
      <div class="current-contract__progress">
        <div class="current-contract__progress__title">
          <div class="current-contract__date">{{loc('options_program_12')}} <span> {{contractState.ExpirationDate.toLocaleString("ru", options)}}</span></div>
          <div class="current-contract__progress-value">{{currentProgress}}%</div>
        </div>
        <div class="current-contract__progress-bar">
          <div 
            :class="[{active: item <= ((currentProgress / 100) * 36)}, 'current-contract__progress-bar__item']"
            v-for="(item, index) in 36"
            :key="index"
          ></div>
        </div>      
      </div>
      <button class="item__btn" v-if="contractState.ExpirationDate < new Date(Date.now())" @click="acceptContract">Start the contract again</button>
    </template>
    <div class="current-contract__data" v-else>
      <div class="current-contract__data-heading">
        <div class="category">section</div>
        <div class="title">requirements</div>
        <div class="subtitle">Short description for example</div>
      </div>
      <div class="current-contract__data-main">
        <div class="current-contract__data-item">
          <div class="current-contract__data-item-heading">
            <div class="current-contract__data-item-desc">{{ loc('options_program_23') }}</div>
            <div class="current-contract__data-item-value">${{ contract.PriceContract }}</div>
          </div>
          <div class="current-contract__data-item-heading">
            <div class="current-contract__data-item-desc">{{ loc('options_program_12') }}</div>
            <div class="current-contract__data-item-value">{{ contract.MinutesToComplete }}{{loc('options_program_22')}}</div>
          </div>
        </div>
        <div class="current-contract__data-item" v-if="this.contract.ContractType === 'Family'">
          <div class="current-contract__data-item-heading">
            <div class="current-contract__data-item-desc">{{ loc('options_program_14') }}</div>
            <div class="current-contract__data-item-value">{{ contract.MinReputation }}</div>
          </div>
          <div class="current-contract__data-item-heading">
            <div class="current-contract__data-item-desc">{{ loc('options_program_15') }}</div>
            <div class="current-contract__data-item-value">{{ contract.MinMembers }}</div>
          </div>
        </div>
      </div>
      <button class="item__btn" @click="acceptContract">Take a contract</button>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapState } from 'vuex'
export default {
  name: 'CurrentContract',
  props: {
    contract: Object
  },
  data: function () {
    return {
      options: {
        month: 'long',
        day: 'numeric',
        hour: 'numeric',
        minute: 'numeric',
      },
    }
  },
  computed: {
    ...mapGetters('localization', ['loc']),
    ...mapState('optionsMenu', ['myContracts', 'familyContracts']),
    contractState() {
      if (this.contract.ContractType === "Family")
        return this.familyContracts.find(c => c.ContractName == this.contract.ContractName);
      if (this.contract.ContractType === "Single")
        return this.myContracts.find(c => c.ContractName == this.contract.ContractName);
      return null;
    },
    currentProgress() {
      if (!this.contractState) return 0;
      return Math.round(this.contractState.CurrentLevel * 100 / this.contract.MaxLevel);
    }
  },
  methods: {
    selectCoordinats: function (coord) {
      if (!this.contractState) return;
      if (coord.Positions.length == 1)
        window.mp.trigger('personalEvents:selectCoordinats', JSON.stringify(coord.Positions[0]));
      else
      {
        console.log(JSON.stringify(this.contractState.PointsVisited))
        console.log(JSON.stringify(coord.Positions))
        for(let i=0; i<coord.Positions.length; i++)
          if (this.contractState.PointsVisited.findIndex(pt => pt.x == coord.Positions[i].x && pt.y == coord.Positions[i].y ) < 0)
          {
            window.mp.trigger('personalEvents:selectCoordinats', JSON.stringify(coord.Positions[i]));
            return;
          }
      }
      
    },
    acceptContract: function() {
      window.mp.triggerServer('personalEvents:acceptContract', this.contract.ContractName)
    }
  }
}
</script>

<style lang="scss" scoped>
.current-contract{
  max-width: 19.6rem;
  min-width: 19.6rem;
  height: 41.5rem;
  overflow-y: auto;
  padding-right: 1rem;

  scrollbar-width: thin;
  scrollbar-color: #5cff80 #444444;

  &::-webkit-scrollbar {
    display: block;
    width: 0.05rem;
    height: 0;
  }
  &::-webkit-scrollbar-track {
    background: #444444;
  }
  &::-webkit-scrollbar-thumb {
    background-color: #a9a9a9;
  }


  &__heading {
    margin: 0 0 1.5rem 0.7rem;
  }

  &__reward{
    width: 100%;
    margin-bottom: 1.5rem;
    &-heading {
      padding: 1rem 1.5rem;
      border: 1px solid rgba(255,255,255,0.1);
      background: radial-gradient(at center bottom, rgba(255,255,255,0.15) 10%, rgba(0,0,0,0) 75%);
      .title {
        font-size: 1.3rem;
        line-height: 1.3;
      }
    }
    &-list{
      display: flex;
      flex-wrap: wrap;
      align-items: start;
      &__item{
        display: flex;
        flex-flow: column nowrap;
        justify-content: space-between;
        align-items: center;
        width: 4.4rem;
        height: 4.4rem;
        padding: 0.3rem;
        margin-top: 0.6rem;
        border: 1px solid #5CFF80;
        overflow: hidden;

        &:not(&:last-child){
          margin-right: 0.225rem;
        }

        &-decorate{
          display: block;
          width: 100%;
          height: 65%;
          position: relative;
          margin-bottom: 0.2rem;
          filter: drop-shadow(0 0 1.5rem #5CFF80);

          & img {
            display: inline-block;
            width: 100%;
            height: 100%;
            object-fit: contain;
          }
        }

        &-value{
          display: block;
          font-weight: 700;
          font-size: .7rem;
          line-height: 1.05rem;
          color: #00FF38;
        }
      }
    }
  }

  &__data {
    &-heading {
      margin: 0 0 1rem 0.7rem;
    }

    &-item {
      width: 100%;
      position: relative;
      display: flex;
      justify-content: space-between;
      padding: 1rem 1.6rem;
      border: 1px solid rgba(255,255,255,0.1);
      background: radial-gradient(at center bottom, rgba(255,255,255,0.15) 10%, rgba(0,0,0,0) 75%);
      margin-bottom: 0.7rem;

      &:before {
        content: '';
        position: absolute;
        height: 1.44rem;
        top: 1rem;
        left: 0.7rem;
        color: #fff;
        background: #fff;
        border: 1px solid #fff;
        box-shadow: 0px 0px 14px rgba(255, 255, 255, 0.55);
      }

      &-heading {
        display: flex;
        flex-flow: column nowrap;
        justify-content: space-between;
        &:first-child {
          margin-right: 0.7rem;
        }
      }
      &-desc {
        font-size: 0.65rem;
        line-height: 0.8rem;
        color: rgba(255,255,255,0.55);
      }
      &-value {
        font-weight: 600;
        font-size: 1.3rem;
        line-height: 1.3;
        color: #fff;
      }
    }
  }


  &__find{
    margin-bottom: 2rem;

    &-heading {
      margin: 0 0 1rem 0.7rem;
    }

    &-list{
      display: grid;
      grid-template-columns: repeat(2, 1fr);
      grid-column-gap: 1rem;
      grid-row-gap: 0.5rem;
      width: 100%;
      .item__btn{
        padding: 0.7rem 3rem;
      }
    }
  }

  &__date{
    span{
      color: rgba(182, 211, 0, 1);
    }
  }

  &__progress{
    display: flex;
    flex-direction: column;
    width: 100%;
    color: #fff;
    &__title{
      width: 100%;
      display: flex;
      align-items: flex-end;
      justify-content: space-between;
      font-weight: 600;
      font-size: 1rem;
      line-height: 1.2rem;
      letter-spacing: 0.03em;
      margin-bottom: .3rem;
    }
    &-bar{
      width: 100%;
      display: flex;
      align-items: center;
      justify-content: space-between;
      height: 2rem;
      margin: 0.2rem 0 2rem;
      &__item{
        height: 100%;
        width: .2rem;
        background: rgba(255, 255, 255, 0.5);
        &.active{
          background: rgba(255, 255, 255, 1);
        }
      }
    }
  }
}
</style>
