//
//  UnityThinkGear.m
//  Unity-iPhone
//
//  Created by NeuroSky on 3/27/13.
//
//

#import "UnityThinkGear.h"




@implementation UnityThinkGear

- (id)initWithRawEnable:(bool)rawEnable{
    self = [super init];
    if(self){
        [self setSendRawEnable:rawEnable];
        [self setSendEEGEnable:true];
        [self setSendESenseEnable:true];
        [self setSendBlinkEnable:true];
        
        
        [[MWMDevice sharedInstance] setDelegate:self];
        NSLog(@"MWMDevice version = %@",[MWMDevice sharedInstance] .getVersion);
        //sdk.delegate = self;
        //enable console log here
        [[MWMDevice sharedInstance] enableConsoleLog:YES];
        deviceInfo = @"";
        NSLog(@"init MWMDevice");
        
        
        [self initEEGSDK];
        
        NSLog(@"call Algo SDK startProcess");
        [[NskAlgoSdk sharedInstance] startProcess];
        
    }
    return self;
}

#pragma MWM SDK DELEGATE
// MWM SDK DELEGATE
-(void)deviceFound:(NSString *)devName MfgID:(NSString *)mfgID DeviceID:(NSString *)deviceID{
    deviceInfo = [NSString stringWithFormat:@"%@;%@;%@",mfgID,devName,deviceID];
    
    
    //    deviceInfo = [NSString stringWithFormat:@"devName = %@,MfgID = %@ ,deviceID = %@",devName,mfgID,deviceID];
    //    deviceInfo = [deviceInfo stringByAppendingString:[NSString stringWithFormat:@"devName = %@,MfgID = %@ ,deviceID = %@ \n",devName,mfgID,deviceID]];
    
    NSLog(@"deviceFound deviceInfo = %@",deviceInfo);
    const char * info =[deviceInfo UTF8String];
    if ([deviceID containsString:@":"]) {
        bleFlag = NO;
    }
    else{
        bleFlag = YES;

    }
    
    UnitySendMessage("ThinkGear", "receiveDeviceInfo", info);
}

-(void)didConnect{
    NSLog(@"OC didConnect ----");
}


-(void)didDisconnect{
    NSLog(@"OC didDisconnect ----");
}

-(void)eegSample:(int) sample{
    if(sendRawEnable){
        UnitySendMessage("ThinkGear", "receiveRawdata", [[NSString stringWithFormat:@"%d",sample] cStringUsingEncoding:NSUTF8StringEncoding]);
    }
    int16_t eeg_data[1];
    eeg_data[0] = (int16_t)sample;
    // if BLE device, double the data;256->512 sample rate
    if (bleFlag) {
        [[NskAlgoSdk sharedInstance]  dataStream:NskAlgoDataTypeEEG data:eeg_data length:1];
    }
    [[NskAlgoSdk sharedInstance]  dataStream:NskAlgoDataTypeEEG data:eeg_data length:1];
}

-(void)eSense:(int)poorSignal Attention:(int)attention Meditation:(int)meditation{
    NSLog(@"OC poorSignal = %d",poorSignal);
    UnitySendMessage("ThinkGear", "receivePoorSignal", [[NSString stringWithFormat:@"%d",poorSignal] cStringUsingEncoding:NSUTF8StringEncoding]);
    
    if(sendESenseEnable){
        UnitySendMessage("ThinkGear", "receiveAttention", [[NSString stringWithFormat:@"%d",attention] cStringUsingEncoding:NSUTF8StringEncoding]);
        UnitySendMessage("ThinkGear", "receiveMeditation", [[NSString stringWithFormat:@"%d",meditation] cStringUsingEncoding:NSUTF8StringEncoding]);
    }
    
    
    int16_t poor_signal_data[1];
    poor_signal_data[0] = (int16_t)poorSignal;
    [[NskAlgoSdk sharedInstance] dataStream:NskAlgoDataTypePQ data:poor_signal_data length:1];
    
    int16_t attention_data[1];
    attention_data[0] = (int16_t)attention;
     [[NskAlgoSdk sharedInstance] dataStream:NskAlgoDataTypeAtt data:attention_data length:1];
    
    int16_t meditation_data[1];
    meditation_data[0] = (int16_t)meditation;
     [[NskAlgoSdk sharedInstance] dataStream:NskAlgoDataTypeMed data:meditation_data length:1];
}

-(void)eegPowerDelta:(int)delta Theta:(int)theta LowAlpha:(int)lowAlpha HighAlpha:(int)highAlpha{
    if(sendEEGEnable){
        UnitySendMessage("ThinkGear", "receiveDelta", [[NSString stringWithFormat:@"%d",delta] cStringUsingEncoding:NSUTF8StringEncoding]);
        UnitySendMessage("ThinkGear", "receiveTheta", [[NSString stringWithFormat:@"%d",theta] cStringUsingEncoding:NSUTF8StringEncoding]);
        UnitySendMessage("ThinkGear", "receiveLowAlpha", [[NSString stringWithFormat:@"%d",lowAlpha] cStringUsingEncoding:NSUTF8StringEncoding]);
        UnitySendMessage("ThinkGear", "receiveHighAlpha", [[NSString stringWithFormat:@"%d",highAlpha] cStringUsingEncoding:NSUTF8StringEncoding]);
        
        
    }
    
    
}

-(void)eegPowerLowBeta:(int)lowBeta HighBeta:(int)highBeta LowGamma:(int)lowGamma MidGamma:(int)midGamma{
    if(sendEEGEnable){
        UnitySendMessage("ThinkGear", "receiveLowBeta", [[NSString stringWithFormat:@"%d",lowBeta] cStringUsingEncoding:NSUTF8StringEncoding]);
        UnitySendMessage("ThinkGear", "receiveHighBeta", [[NSString stringWithFormat:@"%d",highBeta] cStringUsingEncoding:NSUTF8StringEncoding]);
        UnitySendMessage("ThinkGear", "receiveLowGamma", [[NSString stringWithFormat:@"%d",lowGamma] cStringUsingEncoding:NSUTF8StringEncoding]);
        UnitySendMessage("ThinkGear", "receiveHighGamma", [[NSString stringWithFormat:@"%d",midGamma] cStringUsingEncoding:NSUTF8StringEncoding]);
    }
    
    
    
}

-(void)eegBlink:(int) blinkValue{
    UnitySendMessage("ThinkGear", "receiveBlink", [[NSString stringWithFormat:@"%d",blinkValue] cStringUsingEncoding:NSUTF8StringEncoding]);
}







//- (void)accessoryDidConnect:(EAAccessory *)accessory {
//    // start the data stream to the accessory
////    [[TGAccessoryManager sharedTGAccessoryManager] startStream];
//}

- (void)accessoryDidDisconnect {
    
}

- (void)dataReceived:(NSDictionary *)data {
    //[data retain];
    
    if(sendRawEnable){
        UnitySendMessage("ThinkGear", "receiveRawdata", [[NSString stringWithFormat:@"%d",[[data valueForKey:@"raw"] shortValue]] cStringUsingEncoding:NSUTF8StringEncoding]);
    }
    
    if([data valueForKey:@"blinkStrength"] && sendBlinkEnable){
        UnitySendMessage("ThinkGear", "receiveBlink", [[NSString stringWithFormat:@"%d",[[data valueForKey:@"blinkStrength"] intValue]] cStringUsingEncoding:NSUTF8StringEncoding]);
    }
    
    if([data valueForKey:@"eSenseAttention"]){
        UnitySendMessage("ThinkGear", "receivePoorSignal", [[NSString stringWithFormat:@"%d",[[data valueForKey:@"poorSignal"] intValue]] cStringUsingEncoding:NSUTF8StringEncoding]);
        
        if(sendESenseEnable){
            UnitySendMessage("ThinkGear", "receiveAttention", [[NSString stringWithFormat:@"%d",[[data valueForKey:@"eSenseAttention"] intValue]] cStringUsingEncoding:NSUTF8StringEncoding]);
            UnitySendMessage("ThinkGear", "receiveMeditation", [[NSString stringWithFormat:@"%d",[[data valueForKey:@"eSenseMeditation"] intValue]] cStringUsingEncoding:NSUTF8StringEncoding]);
        }
        
        if(sendEEGEnable){
            UnitySendMessage("ThinkGear", "receiveDelta", [[NSString stringWithFormat:@"%f",[[data valueForKey:@"eegDelta"] floatValue]] cStringUsingEncoding:NSUTF8StringEncoding]);
            UnitySendMessage("ThinkGear", "receiveTheta", [[NSString stringWithFormat:@"%f",[[data valueForKey:@"eegTheta"] floatValue]] cStringUsingEncoding:NSUTF8StringEncoding]);
            UnitySendMessage("ThinkGear", "receiveLowAlpha", [[NSString stringWithFormat:@"%f",[[data valueForKey:@"eegLowAlpha"] floatValue]] cStringUsingEncoding:NSUTF8StringEncoding]);
            UnitySendMessage("ThinkGear", "receiveHighAlpha", [[NSString stringWithFormat:@"%f",[[data valueForKey:@"eegHighAlpha"] floatValue]] cStringUsingEncoding:NSUTF8StringEncoding]);
            UnitySendMessage("ThinkGear", "receiveLowBeta", [[NSString stringWithFormat:@"%f",[[data valueForKey:@"eegLowBeta"] floatValue]] cStringUsingEncoding:NSUTF8StringEncoding]);
            UnitySendMessage("ThinkGear", "receiveHighBeta", [[NSString stringWithFormat:@"%f",[[data valueForKey:@"eegHighBeta"] floatValue]] cStringUsingEncoding:NSUTF8StringEncoding]);
            UnitySendMessage("ThinkGear", "receiveLowGamma", [[NSString stringWithFormat:@"%f",[[data valueForKey:@"eegLowGamma"] floatValue]] cStringUsingEncoding:NSUTF8StringEncoding]);
            UnitySendMessage("ThinkGear", "receiveHighGamma", [[NSString stringWithFormat:@"%f",[[data valueForKey:@"eegHighGamma"] floatValue]] cStringUsingEncoding:NSUTF8StringEncoding]);
        }
    }
}//end (void)dataReceived

- (bool)getSendRawEnable{
    NSLog(@"Raw");
    return sendRawEnable;
}
- (bool)getSendEEGEnable{
    NSLog(@"EEG");
    return sendEEGEnable;
}
- (bool)getSendESenseEnable{
    NSLog(@"ESense");
    return sendESenseEnable;
}

- (bool)getSendBlinkEnable{
    NSLog(@"Blink");
    return sendBlinkEnable;
}

- (void)setSendRawEnable:(bool)value{
    value?NSLog(@"SetRaw:True"):NSLog(@"SetRaw:False");
    sendRawEnable = value;
}
- (void)setSendEEGEnable:(bool)value{
    value?NSLog(@"SetEEG:True"):NSLog(@"SetEEG:False");
    sendEEGEnable = value;
}
- (void)setSendESenseEnable:(bool)value{
    value?NSLog(@"SetESense:True"):NSLog(@"SetSEnse:False");
    sendESenseEnable = value;
}
- (void)setSendBlinkEnable:(bool)value{
    value?NSLog(@"SetBlink:True"):NSLog(@"SetBlink:False");
    sendBlinkEnable = value;
    
}




///=====  EEG ALGO SDK Part


- (void)initEEGSDK{
    NskAlgoSdk *handle = [NskAlgoSdk sharedInstance];
    handle.delegate = self;
    
    NskAlgoEegType algoTypes = 0;
    algoTypes |= NskAlgoEegTypeAtt;
    algoTypes |= NskAlgoEegTypeMed;
    algoTypes |= NskAlgoEegTypeBP;
    algoTypes |= NskAlgoEegTypeBlink;

    NSInteger ret;
    if ((ret = [[NskAlgoSdk sharedInstance] setAlgorithmTypes:algoTypes]) != 0) {
        
        NSLog(@"Fail to init EEG SDK [%ld]",ret);
        //            return;
    }
    else{
        NSLog(@"init EEG SDK success");
        NSLog(@"algo sdk version = %@",[[NskAlgoSdk sharedInstance] getSdkVersion]);
        
    }


}

#pragma EEG SDK DELEGATE

/* notification on SDK state change */
- (void) stateChanged: (NskAlgoState)state reason:(NskAlgoReason)reason{
    //when algo sdk automatically stop, we should restart it.
    if (state == NskAlgoStateStop && reason == NskAlgoReasonSignalQuality) {
        NSLog(@"call Algo SDK startProcess");
        [[NskAlgoSdk sharedInstance] startProcess];
        
    }



}


/* notification on EEG algorithm index */
- (void) attAlgoIndex: (NSNumber*)att_index{
      UnitySendMessage("ThinkGear", "receiveEEGAlgorithmValue", [[NSString stringWithFormat:@"attention:%@",att_index]  cStringUsingEncoding:NSUTF8StringEncoding]);
}

- (void) medAlgoIndex: (NSNumber*)med_index{
    UnitySendMessage("ThinkGear", "receiveEEGAlgorithmValue", [[NSString stringWithFormat:@"meditation:%@",med_index]  cStringUsingEncoding:NSUTF8StringEncoding]);


}

- (void) eyeBlinkDetect: (NSNumber*)strength{


}

- (void) bpAlgoIndex: (NSNumber*)delta theta:(NSNumber*)theta alpha:(NSNumber*)alpha beta:(NSNumber*)beta gamma:(NSNumber*)gamma{
    UnitySendMessage("ThinkGear", "receiveEEGAlgorithmValue", [[NSString stringWithFormat:@"delta:%@",delta]  cStringUsingEncoding:NSUTF8StringEncoding]);
    UnitySendMessage("ThinkGear", "receiveEEGAlgorithmValue", [[NSString stringWithFormat:@"theta:%@",theta]  cStringUsingEncoding:NSUTF8StringEncoding]);
    UnitySendMessage("ThinkGear", "receiveEEGAlgorithmValue", [[NSString stringWithFormat:@"alpha:%@",alpha]  cStringUsingEncoding:NSUTF8StringEncoding]);
    UnitySendMessage("ThinkGear", "receiveEEGAlgorithmValue", [[NSString stringWithFormat:@"beta:%@",beta]  cStringUsingEncoding:NSUTF8StringEncoding]);
    UnitySendMessage("ThinkGear", "receiveEEGAlgorithmValue", [[NSString stringWithFormat:@"gamma:%@",gamma]  cStringUsingEncoding:NSUTF8StringEncoding]);


}

/* notification on signal quality */
- (void) signalQuality: (NskAlgoSignalQuality)signalQuality{
    

}



@end





















